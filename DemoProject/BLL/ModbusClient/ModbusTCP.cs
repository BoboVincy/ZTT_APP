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
    public class ModbusTCP:ModbusBase
    {
        ushort TransID = 0;
        System.Windows.Threading.DispatcherTimer ReadDataTimer = new System.Windows.Threading.DispatcherTimer();
        public override bool Connect(string IP, int Port)
        {
            TCPParameter Param = new TCPParameter();
            Param.IP = IP;
            Param.Port = Port;
            TCPClient = new BaseTCP(Param);
            ReadDataTimer.Tick += new EventHandler(GetTagValue);
            ReadDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            MyDelegate.dSetTCPRecieveTimer += Recieve;
            if (TCPClient.TCPConnect())
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
            if (TCPClient != null)
            {
                byte DN = Convert.ToByte(DeviceNum);
                byte RT = GetRegisterType(RegisterType);
                byte[] DataByte = new byte[12];
                DataByte[0] = (byte)((TransID & 0xff00)>>8);
                DataByte[1] = (byte)(TransID & 0x00ff);
                DataByte[2] = 0;
                DataByte[3] = 0;
                DataByte[4] = 0;
                DataByte[5] = 6;
                DataByte[6] = (byte)(DeviceNum & 0xff);
                DataByte[7] = (byte)(RT & 0xff);
                DataByte[8] = (byte)((StartBit & 0xff00) >> 8);
                DataByte[9] = (byte)(StartBit & 0x00fe);
                DataByte[10] = (byte)((Length & 0xff00) >> 8);
                DataByte[11] = (byte)(Length & 0x00ff);
                TCPClient.TCPSend(DataByte);
                TransID = (ushort)(TransID + 1);
            }
        }

        private void GetTagValue(object Sender, EventArgs E)
        {
            TagValueList = new List<ushort>();
            byte[] RecieveData = TCPClient.TCPRecieve();
            if (RecieveData != null)
            {
                for (int i = 9; i < RecieveData.Length; i = i + 2)
                {
                    ushort Buff = (ushort)((RecieveData[i] << 8) | RecieveData[i + 1]);
                    TagValueList.Add(Buff);
                }
            }
        }

        public override void Recieve()
        {
            if (!ReadDataTimer.IsEnabled)
            {
                ReadDataTimer.Start();
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
    }
}
