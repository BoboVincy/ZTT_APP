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
    public class ModbusBase
    {
        public BaseSerial Serial = null;
        public List<ushort> TagValueList = null;
        public BaseTCP TCPClient = null;
        public virtual bool Connect(string PortName, string BuadRate, string DataBit, string Parity, string StopBit)
        {
            return true;
        }
        public virtual bool Connect(string IP, int Port)
        {
            return true;
        }

        public virtual void Send(int DeviceNum, string RegisterType, ushort StartBit, ushort Length)
        {

        }


        public virtual void Recieve()
        {

        }
    }
}
