
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Common.Log4net;

namespace Common.TCP
{
    public class BaseTCP
    {
        TcpClient Client;
        TCPParameter Param;
        byte[] RecieveBuffer;
        byte[] TempBuffer;
        Socket Listener;
        public BaseTCP(TCPParameter Param)
        {
            Client = new TcpClient();
            this.Param = Param;
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Listener.Bind(new IPEndPoint(IPAddress.Parse(Param.IP), Param.Port));
            //Listener.Listen(20);
            //RecieveBuffer = new byte[500];

        }

        public bool TCPConnect()
        {
            try
            {
                IPEndPoint EndPoint = new IPEndPoint(IPAddress.Parse(Param.IP), Param.Port);
                Listener.Connect(EndPoint);
                //Client.Connect(Param.IP, Param.Port);
                return true;
            }
            catch (Exception e)
            {
                LogHelp.Log.Error(e.ToString());
                return false;
            }
        }

        public bool TCPSend(byte[] Data)
        {
            try
            {
                Listener.Send(Data);
                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
            //NetworkStream NetSteam = Client.GetStream();
            //if (NetSteam.CanWrite)
            //{
            //    NetSteam.Write(Data, 0, Data.Length);
            //    return true;
            //}
            //else
            //{
            //    LogHelp.Log.Error("无法写入数据流！");
            //    return false;
            //}
        }

        public byte[] TCPRecieve()
        {
            RecieveBuffer = new byte[500];
            int RecieveCount = Listener.Receive(RecieveBuffer);
            TempBuffer = new byte[RecieveCount];
            Buffer.BlockCopy(RecieveBuffer, 0, TempBuffer, 0, RecieveCount);
            return TempBuffer;
        }

        public async Task StartListen(int MaxClientNum)
        {
            await ListenTask(MaxClientNum);
        }

        private Task ListenTask(int MaxClientNum)
        {
            try
            {
                var ListenTask = Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {
                            Thread.Sleep(500);
                            Socket RecieveSocket = Listener.Accept();
                            int RecieveCount = RecieveSocket.Receive(RecieveBuffer);
                            TempBuffer = new byte[RecieveCount];
                            Buffer.BlockCopy(RecieveBuffer, 0, TempBuffer, 0, RecieveCount);
                            if (TempBuffer.Length != 0)
                            {

                            }
                        }
                        catch (Exception EX)
                        {
                            break;
                        }
                    }
                });
                return ListenTask;
            }
            catch (Exception E)
            {
                return null;
            }
            
        }

        
    }
}
