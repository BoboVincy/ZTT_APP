using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serial;
using Common.TCP;
using System.IO.Ports;

namespace BLL.ModbusClient
{
    public class ModbusRTU:ModbusBase
    {


        public override bool Connect(string PortName, string BuadRate, string DataBit, string Parity, string StopBit)
        {
            SerialParameter Param = new SerialParameter();
            Param.PortName = PortName;
            Param.BaudRate = Convert.ToInt32(BuadRate);
            Param.DataBits = Convert.ToInt32(DataBit);
            Param.Parity = GetParity(Parity);
            Param.StopBits = GetStopBit(StopBit);
            Param.Encoding = Encoding.ASCII;
            Serial = new BaseSerial(Param);
            Serial.DataReceived += SerialDataReceived;
            if (Serial.OpenPort())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void Send(int DeviceNum, string RegisterType, ushort StartBit, ushort Length)
        {
            if (Serial != null)
            {
                byte DN = Convert.ToByte(DeviceNum);
                byte RT = GetRegisterType(RegisterType);
                byte[] DataByte = new byte[6];
                int Length1 = DataByte.Length;
                byte[] AllDataByte = new byte[8];
                DataByte[0] = (byte)(DN & 0xff);
                DataByte[1] = (byte)(RT & 0xff);
                DataByte[2] = (byte)((StartBit & 0xff00) >> 8);
                DataByte[3] = (byte)(StartBit & 0x00fe);
                DataByte[4] = (byte)((Length & 0xff00) >> 8);
                DataByte[5] = (byte)(Length & 0x00ff);
                byte[] CRCByte = GetCRC16(DataByte);
                for (int i = 0; i < 6; i++)
                {
                    AllDataByte[i] = DataByte[i];
                }
                for (int i = 0; i < 2; i++)
                {
                    AllDataByte[Length1 + i] = CRCByte[i];
                }

                Serial.Write(AllDataByte);

                //byte[] AllDataByte1 = new byte[8] { 1, 3,0 ,0,0, 2, 196, 11 };
                //Serial.Write(AllDataByte1);
            }
        }



        public void SerialDataReceived(object Sender, SerialDataReceivedEventArgs E)
        {
            int DataNum = Serial.baseSerialPort.BytesToRead;
            TagValueList = new List<ushort>();
            byte[] RecieveBuffer = new byte[DataNum];
            for (int i = 0; i < DataNum; i++)
            {
                byte Buffer = Convert.ToByte(Serial.baseSerialPort.ReadByte());
                RecieveBuffer[i] = Buffer;
            }
            for (int i = 3; i < RecieveBuffer.Length - 2; i = i + 2)
            {
                ushort Buff = (ushort)((RecieveBuffer[i] << 8) | RecieveBuffer[i + 1]);
                TagValueList.Add(Buff);
            }
        }

        public byte GetRegisterType(string RegisterType)
        {
            byte RT = 0;
            switch (RegisterType)
            {
                case "1x":
                    RT = Convert.ToByte(1);
                    break;
                case "2x":
                    RT = Convert.ToByte(2);
                    break;
                case "3x":
                    RT = Convert.ToByte(3);
                    break;
                case "4x":
                    RT = Convert.ToByte(4);
                    break;
            }
            return RT;
        }

        private Parity GetParity(string MyParity)
        {
            switch (MyParity)
            {
                case "奇":
                    return Parity.Odd;
                case "偶":
                    return Parity.Even;
                case "无":
                    return Parity.None;
            }
            return Parity.Space;
        }

        private StopBits GetStopBit(string MyStopBit)
        {
            switch (MyStopBit)
            {
                case "0":
                    return StopBits.None;
                case "1":
                    return StopBits.One;
                case "1.5":
                    return StopBits.OnePointFive;
                case "2":
                    return StopBits.Two;
            }
            return StopBits.None;
        }

        public byte[] GetCRC16(byte[] Data)
        {
            int DataLenth = Data.Length;
            if (DataLenth > 0)
            {
                ushort CRC = 0xFFFF;
                for (int i = 0; i < DataLenth; i++)
                {
                    CRC = (ushort)(CRC ^ Data[i]);
                    for (int j = 0; j < 8; j++)
                    {
                        CRC = (CRC & 1) == 1 ? (ushort)((CRC >> 1) ^ 0xA001) : (ushort)(CRC >> 1);
                    }
                }
                byte HI = (byte)((CRC & 0xFF00) >> 8);
                byte Lo = (byte)(CRC & 0x00FF);
                return new byte[] { Lo, HI };
            }
            else
            {
                return new byte[] { 0, 0 };
            }

        }
    }
}
