using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpindleTalker2
{
    static class Spindle
    {
        #region Control packet definitions

        static byte[] ReadCurrentSetF = new byte[] { (byte)Settings.VFD_ModBusID, (byte)CommandType.ReadControlData, (byte)CommandLength.OneByte, (byte)Status.SetF };
        static byte[] RunForward = new byte[] { (byte)Settings.VFD_ModBusID, (byte)CommandType.WriteControlData, (byte)CommandLength.OneByte, (byte)ControlCommands.Run_Fwd };
        static byte[] RunBack = new byte[] { (byte)Settings.VFD_ModBusID, (byte)CommandType.WriteControlData, (byte)CommandLength.OneByte, (byte)ControlCommands.Run_Rev };
        static byte[] stopSpindle = new byte[] { (byte)Settings.VFD_ModBusID, (byte)CommandType.WriteControlData, (byte)CommandLength.OneByte, (byte)ControlCommands.Stop };

        #endregion

        //
        // This is a timer to stop polling (to save CPU cycles) when the spindle is off.
        // If the poll process is stopped at the same time as the spindle stop command is sent
        // the meters will continue to show the running values at the time the stop signal was sent
        //
        private static System.Windows.Forms.Timer pollSpinDown = new System.Windows.Forms.Timer();

        static Spindle()
        {
            pollSpinDown.Interval = 10000; // 10 secs
            pollSpinDown.Tick += pollSpinDown_Tick; 
        }

        public static void Start(SpindleDirection direction)
        {
            //    ModBus packet format [xx] = one byte i.e. 0x1E
            //
            //      [xx]   |     [xx]     |      [xx]      | [xx] [xx] [..] | [xx][xx]
            //    Slave ID | Command Type | Request Length |     Request    |   CRC   
            //
            //    The casting of Enum's below is way overkill but it's to help anyone trying
            //    to get their head around the format of the 'ModBus' protocol used by these
            //    spindles. The CRC is added as part of the SendData() method. 
            //

            Serial.SendData(ReadCurrentSetF); // I'm not sure why this is needed but it seems to be
            SetFrequency(Settings.VFD_MinFreq); 

            // For future testing, the spindle reverse function doesn't appear to be working
            Serial.SendData(direction == SpindleDirection.Forward ? RunForward : RunBack);
        }

        public static void Stop()
        {
            Serial.SendData(stopSpindle);
            Settings.graphsForm.SpindleShuttingDown = true;
            pollSpinDown.Start(); // start a timer to shutdown polling in 10 secs to allow time for the spindle to stop
        }

        public static void SetRPM(int targetRPM)
        {
            //
            //   This function assumes a linear correlation between frequency and spindle speed. This isn't correct but
            //   is a close enough approximation for my purposes. This is a possible area for future development.
            //

            //
            //   Calculate the frequency that equates to the target RPM by working out the target RPM as
            //   a fraction of the max RPM and then multiplying that by the max Frequency.
            //
            int targetFrequency = (int)(((double)targetRPM / (double)Settings.VFD_MaxRPM) * (double)Settings.VFD_MaxFreq);
            SetFrequency(targetFrequency);
        }

        public static void SetFrequency(int targetFrequency)
        {
            //
            //   Check that the target frequency does not exceed the maximum or minumum values for the VFD and/or
            //   spindle. I assume that the VFD will ignore values above max (haven't tested) but values below the
            //   minumum recommended frequency for air-cooled spindles can cause major overheating issues.
            //
            if (targetFrequency < Settings.VFD_MinFreq) targetFrequency = Settings.VFD_MinFreq;
            else if (targetFrequency > Settings.VFD_MaxFreq) targetFrequency = Settings.VFD_MaxFreq;

            targetFrequency = targetFrequency * 100; // VFD expects target frequency in hundredths of Hertz

            Settings.graphsForm.SpindleShuttingDown = false; // Ensure the SetF graph draws properly/

            // Construct the control packet
            byte[] controlPacket = new byte[5];
            controlPacket[0] = (byte)Settings.VFD_ModBusID;
            controlPacket[1] = (byte)CommandType.WriteInverterFrequencyData;
            controlPacket[2] = (byte)CommandLength.TwoBytes;
            controlPacket[3] = (byte)(targetFrequency >> 8); // Bitshift right to get bits nine to 16 of the int32 value
            controlPacket[4] = (byte)targetFrequency; // returns the eight Least Significant Bits (LSB) of the int32 value

            Serial.SendData(controlPacket);
        }


        static void pollSpinDown_Tick(object sender, EventArgs e)
        {
            Serial.StopPolling();
            pollSpinDown.Stop();
        }


    }

}
