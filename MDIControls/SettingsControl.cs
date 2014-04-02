using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpindleTalker2.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpindleTalker2
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBoxQuickset.Text = Settings.QuickSets;
        }

        #region ComPort Initialisation

        // Taken from 
        /* 
         * Project:    SerialPort Terminal
         * Company:    Coad .NET, http://coad.net
         * Author:     Noah Coad, http://coad.net/noah
         * Created:    March 2005
         * 
         * Notes:      This was created to demonstrate how to use the SerialPort control for
         *             communicating with your PC's Serial RS-232 COM Port
         * 
         *             It is for educational purposes only and not sanctified for industrial use. :)
         *             Written to support the blog post article at: http://msmvps.com/blogs/coad/archive/2005/03/23/39466.aspx
         * 
         */
        public void RefreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, false);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }

        public string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }

        /// <summary> Populate the form's controls with default settings. </summary>
        public bool InitializeControlValues()
        {
            bool isCOMPortAvailable = true;

            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            cmbParity.Text = Settings.Parity.ToString();
            cmbStopBits.Text = Settings.StopBits.ToString();
            cmbDataBits.Text = Settings.DataBits.ToString();
            cmbParity.Text = Settings.Parity.ToString();
            cmbBaudRate.Text = Settings.BaudRate.ToString();
            checkBoxAutoConnectAtStartup.Checked = Settings.AutoConnectAtStartup;

            RefreshComPortList();

            // If it is still avalible, select the last com port used
            if (cmbPortName.Items.Contains(Settings.PortName)) cmbPortName.Text = Settings.PortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isCOMPortAvailable = false;
            }

            return isCOMPortAvailable;
        }

        #endregion

        private void SettingsForm_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void cmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.PortName = cmbPortName.SelectedItem.ToString() ;
            Settings.spindleTalkerBase.COMPortStatus(Settings.SerialConnected);
        }

        private void checkBoxAutoConnectAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoConnectAtStartup = checkBoxAutoConnectAtStartup.Checked;
        }

        private void ButtonSaveQuickSet_Click(object sender, EventArgs e)
        {
            Settings.QuickSets = textBoxQuickset.Text;
            Settings.Save();
            Settings.spindleTalkerBase.populateQuickSets();
        }

    }
}
