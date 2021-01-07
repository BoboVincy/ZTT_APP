using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
using System.Net;
using Common.Log4net;

namespace BLL.OTSC
{
    public class GetServer
    {
        private static OPCServer KepServer;
        private static OPCGroups KepGroups;
        private static OPCGroup KepGroup;
        private static OPCBrowser KepBrowers;
        private static OPCItems KepItems;
        private static Array ArrayItemID;
        private static Array ArrayClientHandle;
        private static Array ArrayServerHandle;
        private static Array ArrayReadServerHandle;
        private static Array ArrayError;
        private static Array ArrayReadError;
        private static int TransID = 1;
        private static int CancleID;
        private static List<int> ListClientHandles = new List<int>();
        private static List<int> ListServerHandles = new List<int>();
        private static List<string> ListTempIDs = new List<string>();
        /// <summary>
        /// 存储采集点的值的数组
        /// </summary>
        public string[] TagValueArray ;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object Lock = new object();
        /// <summary>
        /// 非关键字段列表（点名称若包含列表中字段统统不要）
        /// </summary>
        private List<string> NotKeyWordList = new List<string>() { "System", "CommunicationSerialization", "Statistics", "ThingWorx","DataLogger" , "Hints" };
        /// <summary>
        /// 异步读取完成事件注册状态
        /// </summary>
        public bool DataReadEventFlag = false;
        /// <summary>
        /// 数据改变事件注册状态
        /// </summary>
        public bool DataChangedEventFlag = false;

        public GetServer()
        {
            MyDelegate.dSetSubscribeType += SetSubScribeType;
            MyDelegate.dSetReadComplete += SetReadComplete;
        }

        /// <summary>
        /// 获取服务端节点
        /// </summary>
        /// <param name="HostName"></param>
        /// <returns></returns>
        public List<string> GetServerNode(string HostName)
        {
            List<string> ServerNameList = new List<string>();
            try
            {
                KepServer = new OPCServer();
                object ServerNameArray = KepServer.GetOPCServers(HostName);
                foreach (string item in (Array)ServerNameArray)
                {
                    if (!ServerNameList.Contains(item))
                    {
                        ServerNameList.Add(item);
                    }
                }
                return ServerNameList;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("获取OPC服务端节点出错！" + EX.Message);
                return ServerNameList;
            }
        }

        /// <summary>
        /// 获取主机Host名称
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public List<string> GetHostName(string IP)
        {
            List<string> HostNameList = new List<string>();
            try
            {
                IPHostEntry IPEntry = Dns.GetHostEntry(IP);
                for (int i = 0; i < IPEntry.AddressList.Length; i++)
                {
                    string HostName = Dns.GetHostEntry(IPEntry.AddressList[i].ToString()).HostName;
                    if (!HostNameList.Contains(HostName))
                    {
                        HostNameList.Add(HostName);
                    }
                }
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("获取主机Host名称出错！" + EX.Message);
            }
            return HostNameList;
        }

        /// <summary>
        /// 断开服务器连接
        /// </summary>
        /// <returns></returns>
        public bool DisConnect()
        {
            bool Result = true;
            try
            {
                if (KepServer.ServerState == 1)
                {
                    KepServer.Disconnect();
                    Result = true;
                }

                return Result;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("断开服务器连接出错！" + EX.Message);
                Result = false;
                return Result;
            }
        }

        public bool ReConnect(string ServerNode,string ServerIP)
        {
            try
            {
                if (KepServer.ServerState == 6 || KepServer.ServerState == 2)
                {
                    KepServer.Connect(ServerNode, ServerIP);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("OPC重连失败！" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取设备的点
        /// </summary>
        /// <param name="ServerNode">服务端节点</param>
        /// <param name="ServerIP">服务端IP</param>
        /// <returns></returns>
        public List<string> GetDevice(string ServerNode, string ServerIP)
        {
            List<string> AllTagList = new List<string>();
            List<string> TagList = new List<string>();
            try
            {

                if (KepServer.ServerState == 6)
                {
                    KepServer.Connect(ServerNode, ServerIP);
                }
                KepBrowers = KepServer.CreateBrowser();
                KepBrowers.ShowBranches();
                KepBrowers.ShowLeafs(true);
                foreach (object item in KepBrowers)
                {
                    AllTagList.Add(item.ToString());
                }
                int Length = NotKeyWordList.Count();
                foreach (string Tag in AllTagList)
                {
                    int i = 0;
                    foreach (string Item in NotKeyWordList)
                    {
                        if (!Tag.Contains(Item))
                        {
                            i = i + 1;
                        }
                    }
                    if (i == Length)
                    {
                        TagList.Add(Tag);
                    }
                }
                return TagList;
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("获取设备的点出错！" + EX.Message);
                return TagList;
                throw;
            }
        }
        /// <summary>
        /// 获取点的值
        /// </summary>
        /// <param name="TagNameList">需要获取值的点列表</param>
        public void GetTagValue(List<string> TagNameList)
        {

            TagValueArray = new string[TagNameList.Count];
            if (DataChangedEventFlag == true)
            {
                KepGroup.DataChange -= KepGroup_DataChange;
                DataChangedEventFlag = false;
            }
            KepGroups = KepServer.OPCGroups;
            KepGroups.DefaultGroupDeadband = 0;
            KepGroups.DefaultGroupIsActive = true;
            KepGroup = KepGroups.Add(null);
            KepGroup.IsActive = true;
            KepGroup.IsSubscribed = true;
            KepGroup.UpdateRate = 250;
            KepItems = KepGroup.OPCItems;
            SetReadComplete(true);
            ListTempIDs.Clear();
            ListClientHandles.Clear();

            ListTempIDs.Add("0");
            ListClientHandles.Add(0);
            foreach (var item in TagNameList)
            {
                ListTempIDs.Add(item);
            }
            for (int i = 1; i <= TagNameList.Count; i++)
            { 
                ListClientHandles.Add(i);
                //TagNameDic.Add(i, ListTempIDs[i]);
            }
            ArrayItemID = ListTempIDs.ToArray();
            ArrayClientHandle = ListClientHandles.ToArray();
            try
            {
                KepItems.AddItems(TagValueArray.Length, ref ArrayItemID, ref ArrayClientHandle, out ArrayServerHandle, out ArrayError);
                ListServerHandles.Clear();
                ListServerHandles.Add(0);
                for (int i = 0; i < TagValueArray.Length; i++)
                {
                    ListServerHandles.Add(Convert.ToInt32(ArrayServerHandle.GetValue(i + 1)));
                }
                ArrayReadServerHandle = ListServerHandles.ToArray();
                KepGroup.AsyncRead(TagValueArray.Length, ref ArrayReadServerHandle, out ArrayReadError, TransID, out CancleID);
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("获取点的值出错！" + EX.Message);
            }
        }

        /// <summary>
        /// 数据值改变事件发生时的操作
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="ItemValues"></param>
        /// <param name="Qualities"></param>
        /// <param name="TimeStamps"></param>
        private void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                object Value = ItemValues.GetValue(i);
                if (Value != null)
                {
                    TagValueArray[i - 1] = Value.ToString(); ;
                }
            }
            MyDelegate.dWriteToFile();
        }

        /// <summary>
        /// 异步读取完成操作方法
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="ItemValues"></param>
        /// <param name="Qualities"></param>
        /// <param name="TimeStamps"></param>
        /// <param name="Errors"></param>
        private void KepGroup_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                object Value = ItemValues.GetValue(i);
                if (Value != null)
                {
                    TagValueArray[i - 1] = Value.ToString(); 
                }
            }
            MyDelegate.dWriteToFile();
        }

        /// <summary>
        /// 读数据
        /// </summary>
        public void ReadData()
        {
            lock (Lock)
            {
                if (TagValueArray.Length > 0)
                {
                    KepGroup.AsyncRead(TagValueArray.Length, ref ArrayReadServerHandle, out ArrayReadError, TransID, out CancleID);
                }
            }
        }

        /// <summary>
        /// 设置订阅状态，用于委托
        /// </summary>
        /// <param name="State"></param>
        public void SetSubScribeType(bool State)
        {
            if (State == true)
            {
                if (DataChangedEventFlag == false)
                {
                    KepGroup.DataChange += KepGroup_DataChange;
                    DataChangedEventFlag = true;
                }
            }
            else
            {
                if (DataReadEventFlag == true)
                {
                    KepGroup.DataChange -= KepGroup_DataChange;
                    DataChangedEventFlag = false;
                }
            }
        }

        public void SetReadComplete(bool State)
        {
            if (State == true)
            {
                if (DataReadEventFlag == false)
                {
                    KepGroup.AsyncReadComplete += KepGroup_AsyncReadComplete;
                    DataReadEventFlag = true;
                }

            }
            else
            {
                if (DataReadEventFlag == true)
                {
                    KepGroup.AsyncReadComplete -= KepGroup_AsyncReadComplete;
                    DataReadEventFlag = false;
                }
            }
        }
    }
}

