// Icons from http://www.veryicon.com/icons/system/led/
// MODBUS CRC code from http://www.codeproject.com/Articles/19214/CRC-Calculation
// Analogue meter from http://www.codeproject.com/Articles/24945/Analog-Meter
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using SpindleTalker2.Properties;

namespace SpindleTalker2
{
    public partial class MainWindow : Form
    {

        #region Initialisation

        int howLongToWait = 5000;

        public MainWindow()
        {
            InitializeComponent();
        }

        private System.Diagnostics.Stopwatch stopWatchInitialPoll = new System.Diagnostics.Stopwatch();

        private void SpindleTalker_Load(object sender, EventArgs e)
        {
            Settings.spindleTalkerBase = this;
            ChangeGtrackbarColours(false);

            panelMain.Controls.AddRange(new Control[] { Settings.graphsForm, Settings.terminalForm, Settings.settingsForm });
            foreach (Control ctrl in panelMain.Controls)
            {
                ctrl.Dock = DockStyle.Fill;
                if (ctrl.Tag.ToString() == Settings.LastMDIChild)
                {
                    ctrl.BringToFront();
                    foreach(Button button in panelMDI.Controls)
                    if (button.Tag == ctrl.Tag)
                        button.BackColor = SystemColors.GradientActiveCaption;
                        else button.BackColor = SystemColors.Control;
            }


            }

            Settings.settingsForm.InitializeControlValues();

            if(SerialPort.GetPortNames().Length == 0)
            {
                this.Show();
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                COMPortStatus(false);
                return;
            }

            if (Settings.AutoConnectAtStartup)
            {
                Serial.Connect();
                timerInitialPoll.Start();
                stopWatchInitialPoll.Start();
            }

        }

        public void populateQuickSets()
        {
            while (flowLayoutPanelQuickSets.Controls.Count > 0) flowLayoutPanelQuickSets.Controls[0].Dispose();

            string[] quickSetSpeeds = Settings.QuickSets.Split(';');

            foreach (string quickSetSpeed in quickSetSpeeds)
            {
                int targetRPM = Convert.ToInt32(quickSetSpeed);
                Button buttonToAdd = new Button();
                buttonToAdd.Text = Convert.ToInt32(quickSetSpeed).ToString("N0");
                buttonToAdd.Click += buttonSpeed_Click;
                flowLayoutPanelQuickSets.Controls.Add(buttonToAdd);
            }
        }

        #endregion

        #region Button Events

        private void MDI_Select_Click(object sender, EventArgs e)
        {
            Button senderItem = (Button)sender;

            foreach(Button button in panelMDI.Controls)
            {
                if (button == senderItem) button.BackColor = SystemColors.GradientActiveCaption;
                else button.BackColor = SystemColors.Control;
               
            }

            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl.Tag == senderItem.Tag)
                {
                    ctrl.BringToFront();
                    break;
                }
            }

            Settings.LastMDIChild = senderItem.Tag.ToString();

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (Settings.SerialConnected)
            {
                if(Settings.graphsForm.MeterRPM.Value > 0)
                    if (MessageBox.Show("Spindle appears to still be running, are you sure you wish to disconnect?", 
                        "Spindle still running", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                            != System.Windows.Forms.DialogResult.Yes) return;
                
                Serial.Disconnect();
                Thread.Sleep(500);
                Settings.graphsForm.ZeroAll();
                groupBoxSpindleControl.Enabled = false;
                toolStripStatusRPM.Text = "Current RPM Unknown (Not Connected)";
                toolStripStatusRPM.Image = Resources.orangeLED;
            }
            else
            {
                Serial.Connect();
                stopWatchInitialPoll.Reset();
                stopWatchInitialPoll.Start();
                
                timerInitialPoll.Start();
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Spindle.Start((checkBoxReverse.Checked ? SpindleDirection.Backwards : SpindleDirection.Forward));
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            groupBoxSpindleSpeed.Enabled = true;
            ChangeGtrackbarColours(true);
            groupBoxQuickSets.Enabled = true;
            //gTrackBarSpindleSpeed.Value = Settings.VFD_MinFreq;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Spindle.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            groupBoxSpindleSpeed.Enabled = false;
            ChangeGtrackbarColours(false);
            groupBoxQuickSets.Enabled = false;
        }

        private void gTrackBarSpindleSpeed_DoubleClick(object sender, EventArgs e)
        {
            SetSpeedPopup setSpeedPopup = new SetSpeedPopup();
            if (setSpeedPopup.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("OK was clicked");
                Console.WriteLine("Checkbox was " + (setSpeedPopup.checkBoxQuickSets.Checked ? "" : "not ") + "checked");
                Console.WriteLine(setSpeedPopup.textBoxRPM.Text);
            }
            else Console.WriteLine("Cancel was clicked");

        }

        private void buttonSpeed_Click(object sender, EventArgs e)
        {
            string senderText = (sender as Button).Text;
            string targetRPM = Regex.Replace(senderText, "[^.0-9]", "");
            gTrackBarSpindleSpeed.Value = Convert.ToInt32(targetRPM);
        }

        #endregion

        #region Various methods

        private void SpindleTalker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Settings.graphsForm.MeterOutF.Value > 0)
            {
                if(MessageBox.Show("Spindle appears to still be running, are you sure you wish to exit?", "Spindle still running", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1) != System.Windows.Forms.DialogResult.OK)
                { 
                    e.Cancel = true;
                    return;
                }
            }
            Settings.Save();
        }

        public void COMPortStatus(bool connected)
        {
            toolStripStatusLabelComPort.Image = (connected ? Resources.greenLED : Resources.redLED);
            toolStripStatusLabelComPort.Text = Settings.PortName + (connected ? " (connected)" : " (disconnected)");
            toolStripStatusLabelVFDStatus.Image = Resources.orangeLED;
            toolStripStatusLabelVFDStatus.Text = (connected ? "VFD polling" : "VFD Disconnected");
            buttonConnect.Image = (connected ? Resources.connect2 : Resources.disconnect2);
            string status = (connected ? "opened" : "closed");
            Console.WriteLine(string.Format("{0:H:mm:ss.ff} - Port {1} {2}.", DateTime.Now, Settings.PortName, status));
        }

        private void gTrackBarSpindleSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (gTrackBarSpindleSpeed.Value < Settings.VFD_MinRPM) gTrackBarSpindleSpeed.Value = Settings.VFD_MinRPM;
            Spindle.SetRPM(gTrackBarSpindleSpeed.Value);
        }

        // I like this open source trackbar control but it doesn't implement being greyed out when disabled 
        private void ChangeGtrackbarColours(bool Enabled)
        {
            gTrackBar.ColorPack colorPack1 = new gTrackBar.ColorPack();
            gTrackBar.ColorLinearGradient colorLinearGradient1 = new gTrackBar.ColorLinearGradient();
            gTrackBar.ColorLinearGradient colorLinearGradient2 = new gTrackBar.ColorLinearGradient();

            if(Enabled)
            {
                gTrackBarSpindleSpeed.ColorUp = colorPack1;
                colorLinearGradient1.ColorA = System.Drawing.Color.DarkGray;
                colorLinearGradient1.ColorB = System.Drawing.Color.DarkGray;
                this.gTrackBarSpindleSpeed.SliderColorHigh = colorLinearGradient1;
                colorLinearGradient2.ColorA = System.Drawing.Color.Red;
                colorLinearGradient2.ColorB = System.Drawing.Color.Red;
                this.gTrackBarSpindleSpeed.SliderColorLow = colorLinearGradient2;
            }
            else
            {
                colorPack1.Border = System.Drawing.Color.LightGray;
                colorPack1.Face = System.Drawing.Color.LightGray;
                colorPack1.Highlight = System.Drawing.Color.AliceBlue;
                gTrackBarSpindleSpeed.ColorUp = colorPack1;
                colorLinearGradient1.ColorA = System.Drawing.Color.LightGray;
                colorLinearGradient1.ColorB = System.Drawing.Color.LightGray;
                this.gTrackBarSpindleSpeed.SliderColorHigh = colorLinearGradient1;
                colorLinearGradient2.ColorA = System.Drawing.Color.LightGray;
                colorLinearGradient2.ColorB = System.Drawing.Color.LightGray;
                this.gTrackBarSpindleSpeed.SliderColorLow = colorLinearGradient2;
            }
        }

        private void checkBoxReverse_CheckedChanged(object sender, EventArgs e)
        {
            buttonStart.Image = (checkBoxReverse.Checked ? Resources.Reverse : Resources.Start);
        }

        #endregion

        private void timerInitialPoll_Tick(object sender, EventArgs e)
        {
            if (Settings.VFD_MaxFreq > 0 && Settings.VFD_MinFreq >= 0 && Settings.VFD_MaxRPM > 0 && Settings.graphsForm.MeterRPM.Value >= 0)
            {
                timerInitialPoll.Stop();
                Serial.InitialPollFinished();
                Settings.VFD_MaxRPM = (Settings.VFD_MaxRPM / 50) * Settings.VFD_MaxFreq;
                populateQuickSets();
                toolStripStatusLabelVFDStatus.Text = "VFD Settings Downloaded";
                toolStripStatusLabelVFDStatus.Image = Resources.greenLED;
                groupBoxSpindleControl.Enabled = true;
                Thread.Sleep(100);

                if(Settings.graphsForm.MeterRPM.Value > 0)
                {
                    //this.SuspendLayout();
                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                    groupBoxSpindleSpeed.Enabled = true;
                    ChangeGtrackbarColours(true);
                    groupBoxQuickSets.Enabled = true;
                    populateQuickSets();
                    gTrackBarSpindleSpeed.Value = (int)Settings.graphsForm.MeterRPM.Value;
                    //this.ResumeLayout();
                }
            }
            else
            {
                Serial.InitialPoll();
                if(stopWatchInitialPoll.ElapsedMilliseconds > howLongToWait)
                {
                    timerInitialPoll.Stop();

                    if(MessageBox.Show("The VFD does not appear to be responding. Do you wish to continue waiting", "No Response", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        howLongToWait += 5000;
                        stopWatchInitialPoll.Reset();
                        stopWatchInitialPoll.Start();
                        timerInitialPoll.Start();
                    }
                    else
                    {
                        Serial.InitialPollFinished();
                        COMPortStatus(false);
                        toolStripStatusLabelVFDStatus.Text = "VFD did not respond";
                        toolStripStatusLabelVFDStatus.Image = Resources.redLED;
                    }
                }
            }
        }

    }

}
