using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SpindleTalker2.Properties;


namespace SpindleTalker2
{
    public partial class MeterControl : UserControl  
    {
        public MeterControl()
        {
            InitializeComponent();
        }

        public void ProcessPollPacket(byte[] pollPacket)
        {
            int value = Convert.ToInt32((pollPacket[4] << 8) + pollPacket[5]);
            switch(pollPacket[3])
            {
                case (byte)Status.SetF:
                    MeterSetF.Value = (SpindleShuttingDown ? 0 : (double)(value / 100));
                    break;
                case (byte)Status.OutF:
                    MeterOutF.Value = (double)(value / 100);
                    break;
                case (byte)Status.RoTT:
                    MeterRPM.Value = (double)(value);
                    Image toolstripImage = (value > 0 ? Resources.greenLED : Resources.redLED);
                    string status = (value > 0 ? string.Format("Current RPM = {0:#,##0}", value) : "Spindle is stopped");
                    Settings.spindleTalkerBase.toolStripStatusRPM.Text = status;
                    Settings.spindleTalkerBase.toolStripStatusRPM.Image = toolstripImage;
                    break;
                case (byte)Status.OutA:
                    MeterAmps.Value = (double)((double)value / 10);
                    break;
            }
        }

        public void ZeroAll()
        {
            MeterSetF.Value = -1;
            MeterOutF.Value = -1;
            MeterRPM.Value = -1;
        }

        public bool SpindleShuttingDown = false;

    }
}
