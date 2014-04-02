using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpindleTalker2.UserControls
{
    public partial class CommandBuilder : UserControl
    {
        public CommandBuilder()
        {
            InitializeComponent();

            cbCommandType.Items.Clear(); cbCommandType.Items.AddRange(Enum.GetNames(typeof(CommandType)));
            cbCommandLength.Items.Clear(); cbCommandLength.Items.AddRange(Enum.GetNames(typeof(CommandLength)));
            labelSlaveID.Text = Settings.VFD_ModBusID.ToString();

            cbCommandType.SelectedIndex = 0;
            cbCommandLength.SelectedIndex = cbCommandLength.Items.Count - 1;

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            CommandType selectedCommandType = (CommandType)Enum.Parse(typeof(CommandType), cbCommandType.SelectedItem.ToString());
            CommandLength selectedCommandLength = (CommandLength)Enum.Parse(typeof(CommandLength), cbCommandLength.SelectedItem.ToString());

            int packetLength = Convert.ToInt32((byte)selectedCommandLength) + 3;
            byte[] command = new byte[packetLength];
            command[0] = (byte)Settings.VFD_ModBusID;
            command[1] = (byte)selectedCommandType;
            command[2] = (byte)selectedCommandLength;
            command[3] = (byte)data0.Value;
            if (packetLength > 4) command[4] = (byte)Convert.ToByte(data1.Text, 16);
            if (packetLength > 5) command[5] = (byte)Convert.ToByte(data2.Text, 16); 

            Serial.SendData(command);
        }

        private void textBoxEnter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.SelectionStart = textBox.Text.Length;
        }

        private void buttonTick_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently disabled");
            //Settings.terminalForm.textBoxResponse.Text = string.Empty;
            //Settings.terminalForm.textBoxSent.Text = string.Empty;
            //Settings.PIDRunner = true;
            //Timer tick = new Timer();
            //tick.Interval = 50;
            //tick.Tick += tick_Tick;
            //tick.Start();
        }

        void tick_Tick(object sender, EventArgs e)
        {
            //if (data0.Value < 250)
            //{
            //    data0.Value += 1;
            //    buttonSend_Click(null, null);
            //}
            //else
            //{
            //    (sender as Timer).Stop();
            //    Settings.PIDRunner = false;
            //}
        }

        private void buttonClearText_Click(object sender, EventArgs e)
        {
            Settings.terminalForm.textBoxResponse.Clear();
            Settings.terminalForm.textBoxSent.Clear();
        }
    }
}
