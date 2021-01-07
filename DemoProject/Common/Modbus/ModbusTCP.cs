using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Log4net;


namespace Common.Modbus
{
   public class ModbusTCP
    {
        #region 变量

        /// <summary>
        /// IP
        /// </summary>
        private string IP = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        private int Port = 502;

        /// <summary>
        /// Modbus读写对象
        /// </summary>
        ModbusIpMaster master = null;

        /// <summary>
        /// Modbus连接对象
        /// </summary>
        TcpClient client = null;


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IP_m"></param>
        public ModbusTCP(string IP_m)
        {
            IP = IP_m;
            InitTCPModbus();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化一个TCPModbus读写对象
        /// </summary>
        private void InitTCPModbus()
        {
            try
            {
                client = new TcpClient(IP, Port);
                client.SendTimeout = 1;
                client.ReceiveTimeout = 1;
                master = ModbusIpMaster.CreateIp(client);
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("InitTCPModbus", ex);
            }

        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 读多个保持寄存器值
        /// </summary>
        /// <param name="StartAddress"></param>
        /// <param name="Len"></param>
        /// <returns></returns>
        public ushort[] ReadHoldingRegisters(ushort StartAddress, ushort Len)
        {
            ushort[] data = new ushort[Convert.ToInt32(Len)];
            try
            {
                //IsConnect();
                data = master.ReadHoldingRegisters(StartAddress, Len);
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("ReadHoldingRegisters", ex);
                InitTCPModbus();    //重连
            }
            return data;

        }

        /// <summary>
        /// 读单个保持寄存器值
        /// </summary>
        /// <param name="StartAddress"></param>
        /// <returns></returns>
        public ushort ReadHoldingRegisters(ushort StartAddress)
        {
            ushort[] data = new ushort[1];
            data = master.ReadHoldingRegisters(StartAddress, 1);
            return data[0];
        }

        /// <summary>
        /// 向单个保持寄存器写入值
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Value"></param>
        public void WriteSingleHoldingRegister(ushort Address, ushort Value)
        {
            int i = 3;
            while (i-- > 0)
            {
                try
                {
                    master.WriteSingleRegister(Address, Value);
                    Thread.Sleep(50);
                    ushort WriteValue = ReadHoldingRegisters(Address);
                    Thread.Sleep(50);
                    if (Value == WriteValue)
                        return;
                    else
                        continue;
                }
                catch (Exception ex)
                {
                    LogHelp.Log.Error("WriteSingleHoldingRegister", ex);
                    InitTCPModbus();    //重连
                }
            }

        }



        /// <summary>
        /// 清零（先值1再置0）
        /// </summary>
        /// <param name="Address"></param>
        public void ResetNumToZero(ushort Address)
        {
            WriteSingleHoldingRegister(Address, 1);
            WriteSingleHoldingRegister(Address, 0);
        }

        #endregion
    }
}
