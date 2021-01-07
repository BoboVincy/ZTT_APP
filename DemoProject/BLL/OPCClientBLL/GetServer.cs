using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
using System.Net;
using Model.OPCClientModel;

namespace BLL.OPCClientBLL
{
    public static class GetServer
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
        private static int TransID=1;
        private static int CancleID;
        private static List<TagListInfo> TagList = new List<TagListInfo>();
        private static List<int> ListClientHandles = new List<int>();
        private static List<int> ListServerHandles = new List<int>();
        private static List<string> ListTempIDs = new List<string>();
        private static List<DeviceProperty> ListDevice = new List<DeviceProperty>();
        private static object Lock = new object();


        public static List<string> GetServerName(string Node)
        {
            List<string> ServerNameList = new List<string>();
            try
            {
                KepServer = new OPCServer();
                object ServerNameArray = KepServer.GetOPCServers(Node);
                foreach (string item in (Array)ServerNameArray)
                {
                    if (!ServerNameList.Contains(item))
                    {
                        ServerNameList.Add(item);
                    }
                }
                return ServerNameList;
            }
            catch (Exception)
            {
                MyDelegate.dShowMessageBox("获取服务器失败", "注意");
                return ServerNameList;
                throw;
            }
        }

        public static List<string> GetServerNode()
        {
            List<string> NodeList = new List<string>();
            try
            {
                IPHostEntry IPEntry = Dns.GetHostEntry(Environment.MachineName);
                for (int i = 0; i < IPEntry.AddressList.Length; i++)
                {
                    string HostNode = Dns.GetHostEntry(IPEntry.AddressList[i].ToString()).HostName;
                    if (!NodeList.Contains(HostNode))
                    {
                        NodeList.Add(HostNode);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NodeList;
        }

        public static bool GetServerConnection(string Node, string Name)
        {
            bool Result = false;
            try
            {
                KepServer.Connect(Name, Node);
                KepServer.Disconnect();
                Result = true;
                return Result;
            }
            catch (Exception)
            {
                return Result;
            }
        }

        public static List<string> GetDevice(string Node, string Name)
        {
            List<string> DeviceList = new List<string>();
            try
            {
                KepServer.Connect(Name, Node);
                KepGroups = KepServer.OPCGroups;
                KepGroups.DefaultGroupDeadband = 0;
                KepGroups.DefaultGroupIsActive = true;
                KepGroup = KepGroups.Add("Group1");
                KepGroup.IsActive = true;
                KepGroup.IsSubscribed = true;
                KepGroup.UpdateRate = 250;
                KepGroup.AsyncReadComplete += KepGroup_AsyncReadComplete;
                KepBrowers = KepServer.CreateBrowser();
                KepBrowers.ShowBranches();
                KepBrowers.ShowLeafs(true);
                foreach (object item in KepBrowers)
                {
                    DeviceList.Add(item.ToString());
                }
                return DeviceList;
            }
            catch (Exception e)
            {
                //return ListDevice;
                return DeviceList;
                throw;
            }
        }


        //public static List<DeviceProperty> GetDevice(string Node, string Name)
        //{
        //    List<string> DeviceList = new List<string>();
        //    try
        //    {
        //        KepServer.Connect(Name, Node);
        //        KepGroups = KepServer.OPCGroups;
        //        KepGroups.DefaultGroupDeadband = 0;
        //        KepGroups.DefaultGroupIsActive = true;
        //        KepGroup = KepGroups.Add("Group1");
        //        KepGroup.IsActive = true;
        //        KepGroup.IsSubscribed = true;
        //        KepGroup.UpdateRate = 250;
        //        KepGroup.AsyncReadComplete += KepGroup_AsyncReadComplete;
        //        KepBrowers = KepServer.CreateBrowser();
        //        KepBrowers.ShowBranches();
        //        KepBrowers.ShowLeafs(true);
        //        foreach (object item in KepBrowers)
        //        {

        //            DeviceList.Add(item.ToString());
        //        }
        //        foreach (string item in DeviceList)
        //        {
        //            string[] DeviceArray = item.Split('.');
        //            switch (DeviceArray.Length)
        //            {
        //                case 2:
        //                    ListDevice.Add(new DeviceProperty
        //                    {
        //                        Channel = DeviceArray[0],
        //                        Device = DeviceArray[1]
        //                    });
        //                    break;

        //                case 3:
        //                    ListDevice.Add(new DeviceProperty
        //                    {
        //                        Channel = DeviceArray[0],
        //                        Device = DeviceArray[1],
        //                        TagName = DeviceArray[2]
        //                    });
        //                    break;
        //                case 4:
        //                    ListDevice.Add(new DeviceProperty
        //                    {
        //                        Channel = DeviceArray[0],
        //                        Device = DeviceArray[1],
        //                        TagName = DeviceArray[2],
        //                        DataType = DeviceArray[3]
        //                    });
        //                    break;
        //            }
        //        }
        //        return ListDevice;
        //    }
        //    catch (Exception e)
        //    {
        //        return ListDevice;
        //        throw;
        //    }
        //}

        public static List<TagListInfo> GetTagValue(string TagName)
        {
            TagList.Add(new TagListInfo {
                Tag = TagName,
            });
            ListTempIDs.Clear();
            ListClientHandles.Clear();
            ListTempIDs.Add("0");
            ListClientHandles.Add(0);
            for (int i = 0; i < TagList.Count(); i++)
            {
                ListTempIDs.Add(TagList[i].Tag);
                ListClientHandles.Add(i + 1);
            }
            ArrayItemID =(Array) ListTempIDs.ToArray();
            ArrayClientHandle = ListClientHandles.ToArray();
            KepGroup.IsSubscribed = true;
            KepItems = KepGroup.OPCItems;
            try
            {
                KepItems.AddItems(TagList.Count(), ref ArrayItemID, ref ArrayClientHandle, out ArrayServerHandle, out ArrayError);
                ListServerHandles.Clear();
                ListServerHandles.Add(0);
                for (int i = 0; i < TagList.Count(); i++)
                {
                    ListServerHandles.Add(Convert.ToInt32(ArrayServerHandle.GetValue(i + 1)));
                }
                ArrayReadServerHandle = (Array)ListServerHandles.ToArray();
                KepGroup.AsyncRead(TagList.Count(), ref ArrayReadServerHandle, out ArrayReadError, TransID, out CancleID);
            }
            catch (Exception EX)
            {
                MyDelegate.dShowMessageBox(EX.ToString(), "提示");
            }
            
            return TagList;
        }


        private static void KepGroup_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            for (int i = 0; i < NumItems; i++)
            {
                object Value = ItemValues.GetValue(i+1);
                if (Value != null)
                {
                    TagList[i].Value = Value.ToString();
                    TagList[i].UpdateTime = (DateTime)TimeStamps.GetValue(i+1);
                }
            }
        }

        public static void ReadData()
        {
            lock (Lock)
            {
                if (TagList.Count > 0)
                {
                    KepGroup.AsyncRead(TagList.Count(), ref ArrayReadServerHandle, out ArrayReadError, TransID, out CancleID);
                }
            }
        }
    }
}
