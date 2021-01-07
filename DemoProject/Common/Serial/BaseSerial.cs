using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Common.Log4net;

namespace Common.Serial
{
    public class BaseSerial
    {
        public SerialPort baseSerialPort;

        public event SerialDataReceivedEventHandler DataReceived;
        public event SerialErrorReceivedEventHandler ErrorReceived;
        public BaseSerial(SerialParameter parameter)
        {
            baseSerialPort = new SerialPort();
            baseSerialPort.PortName = parameter.PortName;
            baseSerialPort.BaudRate = parameter.BaudRate;
            baseSerialPort.Parity = parameter.Parity;
            baseSerialPort.DataBits = parameter.DataBits;
            baseSerialPort.StopBits = parameter.StopBits;
            baseSerialPort.Encoding = parameter.Encoding;
            baseSerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceived);//注册事件
            baseSerialPort.ErrorReceived += new SerialErrorReceivedEventHandler(SerialErroDataReceived);
        }

        public bool OpenPort()
        {
            bool flag = false;
            try
            {
                baseSerialPort.Close();
                baseSerialPort.Open();
                baseSerialPort.DiscardInBuffer();
                baseSerialPort.DiscardOutBuffer();
                Console.WriteLine(string.Format("成功打开端口{0}, 波特率{1}。", baseSerialPort.PortName, baseSerialPort.BaudRate.ToString()));
                flag = true;
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("打开串口失败: " + ex.Message);
            }
            return flag;
        }

        public bool ClosePort()
        {
            bool flag = false;
            try
            {
                baseSerialPort.Close();
                Console.WriteLine(string.Format("成功关闭端口{0}。", baseSerialPort.PortName));
                flag = true;
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("关闭串口失败: " + ex.Message);
            }
            return flag;
        }

        /// <summary>
        /// 查找端口
        /// </summary>
        private String[] FindPorts()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 查找端口
        /// </summary>
        public bool FindPort(string driveName)
        {
            var strArr = GetHardName.GetSerialPort();
            var value = (from a in strArr where a.Contains(driveName) select a).FirstOrDefault();
            if (value != null)
            {
                var match = Regex.Match(value, @"\((.*)\)", RegexOptions.Singleline);
                baseSerialPort.PortName = match.Groups[1].Value;
                return true;
            }
            else
            {
                LogHelp.Log.Error("根据驱动名,查找端口失败！！");
            }
            return false;
        }
        //发送hex格式
        public bool Write(byte[] Data)
        {
            if (baseSerialPort == null)
            {
                return false;
            }
            if (baseSerialPort.IsOpen == false)
            {
                LogHelp.Log.Error("串口未打开，无法发送数据！！");
                return false;
            }
            try
            {
                baseSerialPort.Write(Data, 0, Data.Length);
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("发送数据失败！！" + ex.Message);
                return false;
            }
            return true;
        }
        //发送字符串
        private bool Write(string Data)
        {
            if (baseSerialPort == null)
            {
                return false;
            }
            if (baseSerialPort.IsOpen == false)
            {
                LogHelp.Log.Error("串口未打开，无法发送数据！！");
                return false;
            }
            try
            {
                baseSerialPort.Write(Data);
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("发送数据失败！！" + ex.Message);
                return false;
            }
            return true;
        }

        #region 数据接收处理
        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataReceived?.Invoke(this, e);
        }

        private void SerialErroDataReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ErrorReceived?.Invoke(this, e);
        }

        #endregion
    }
}
