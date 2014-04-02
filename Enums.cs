// adapted from http://www.cnczone.com/forums/phase-converters/91847-huanyang-vfd-rs485-modbus-7.html
// Credit - user ScottA

using System;

namespace SpindleTalker2
{
    [Flags]
    enum CommandType
    {
        FunctionRead = 0x01,
        FunctionWrite = 0x02,
        WriteControlData = 0x03,
        ReadControlData = 0x04,
        WriteInverterFrequencyData = 0x05,
    }

    [Flags]
    enum Status
    {
        SetF = 0x00,
        OutF = 0x01,
        OutA = 0x02,
        RoTT = 0x03,
        DCV = 0x04,
        ACV = 0x05,
        Cont = 0x06,
        Tmp = 0x07,
    }

    [Flags]
    enum ControlCommands
    {
        Run_Fwd = 0x01,
        Stop = 0x08,
        Run_Rev = 0x11, // Note that this must be enabled on the VFD - PD??? set to '1'
    }

    [Flags]
    enum CommandLength
    {
        OneByte = 0x01,
        TwoBytes = 0x02,
        ThreeBytes = 0x03,
    }

    public enum ControlResponse
    {
        Run = 0x01,
        Jog = 0x02,
        Command_rf = 0x04,
        Running = 0x08,
        Jogging = 0x10,
        Running_rf = 0x20,
        Braking = 0x40,
        Track_Start = 0x80,
    }

    enum SpindleDirection
    {
        Forward,
        Backwards,
    }

    enum ModbusRegisters
    {
        //Register byte - 0x00 = Set Frequency, 0x01 = Output Frequency, 0x02 = Output Amps, 0x03 = RPM
        SetFreq = 0x00,
        OutputFreq = 0x01,
        OutputAmps = 0x02,
        CurrentRPM = 0x03,
        MaxFreq = 0x05, //PD005
        MinFreq = 0x0B, //PD011
        MaxRPM = 0x90,  //PD144
    }

}
