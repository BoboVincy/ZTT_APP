using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.IBOXClient;
using FBoxClientDriver;
using FBoxClientDriver.Contract;
using FBoxClientDriver.Impl;
using System.Threading;
using Common.MQTT.Model;



namespace BLL.IBOXClientBLL
{
    public class GetIBOXServer
    {
        private string ClientID;
        private string ClientKey;
        private string UserName;
        private string PassWord;
        private string IDServer;
        private string APPServer;
        private string HDataServer;
        private LoginModel LoginParam;
        public Dictionary<string, string> ChangedTagDic;
        IBOXMQTT Client;
        System.Windows.Threading.DispatcherTimer ReadDataTimer = new System.Windows.Threading.DispatcherTimer();
        private static IFBoxClientManager FBOX;
        private static Dictionary<string, FBoxTagInfo> TagDic = new Dictionary<string, FBoxTagInfo>();
        List<FBoxTagInfo> TagList = new List<FBoxTagInfo>();



        public GetIBOXServer(string ClientID,string ClientKey,string UserName,string PassWord,string IDServer,string APPServer,string HDataServer)
        {
            this.ClientID = ClientID;
            this.ClientKey = ClientKey;
            this.UserName = UserName;
            this.PassWord = PassWord;
            this.IDServer = IDServer;
            this.APPServer = APPServer;
            this.HDataServer = HDataServer;
            ChangedTagDic = new Dictionary<string, string>(); 
            LoginParam = new LoginModel()
            {
                Host = "127.0.0.1",
                Port = 11883,
                UserName = "admin",
                PassWord = "public"
            };
            Client = new IBOXMQTT(LoginParam);
            ReadDataTimer.Interval = new TimeSpan(0, 0, 0, 10);
            ReadDataTimer.Tick += new EventHandler(ToMQTT);
            var Crededntial = new DefaultCredentialProvider(ClientID, ClientKey, UserName, PassWord);
            FBOX = new FBoxClientManager(IDServer, APPServer, HDataServer, Crededntial, Guid.NewGuid().ToString("N"), null);
        }

        public void IBOXLogin()
        {
            CancellationTokenSource CTS = new CancellationTokenSource(TimeSpan.FromSeconds(20));
            try
            { 
                Task.Run(async () =>
                {
                    try
                    {
                        await FBOX.Restart();
                        FBOX.BoxConnectStateChanged += FBOXStateChanged;
                        FBOX.DataMonitorValueChanged += FBOXTagChanged;
                        ReadDataTimer.Start();
                        MyDelegate.dShowProcessMessage("登录成功！");
                    }
                    catch (Exception e)
                    {
                        MyDelegate.dShowMessage("登录失败！", "提示");
                    }
                }, CTS.Token);

            }
            catch (Exception e)
            {

            }

           
        }

        public void IBoxOpen(string BoxNum)
        {
            try
            {

                Task.Run(async () =>
                {
                    try
                    {
                        Task.WaitAny();
                        await FBOX.StartAllDataMonitorPointsOnBox(new BoxArgs(BoxNum));
                        MyDelegate.dShowProcessMessage("所有点开启成功！");
                    }
                    catch (Exception e)
                    {

                    }
                });

            }
            catch (Exception)
            {

            }
        }
        private void FBOXStateChanged(object Sender, IList<BoxConnectionStateItem> StateItems)
        {
            foreach (var StateItem in StateItems)
            {
                try
                {
                    if (StateItem.NewState == BoxConnectionState.Connected)
                    {
                        Task.Run(async () =>
                        {
                            await FBOX.StartAllDataMonitorPointsOnBox(new BoxArgs(StateItem.BoxNo));
                        });
                    }
                }
                catch
                {

                }
            }
        }

        private void FBOXTagChanged(object Sender, IList<DataMonitorValueChangedArgs> TagItems)
        {
            if (TagList.Count() != 0)
            {
                foreach (var TagItem in TagItems)
                {
                    if (TagItem.Value == null || !TagDic.Keys.Contains(TagItem.Uid.ToString()))
                    {
                        continue;
                    }
                    TagDic[TagItem.Uid.ToString()].TagValue = TagItem.Value.ToString();
                    ChangedTagDic.Add(DateTime.Now.ToString()+"--"+TagItem.Name, TagItem.Value.ToString());
                }
                MyDelegate.dGetTagInfo(TagList);

            }
        }

        private void ToMQTT(object Sender, EventArgs E)
        {
            Task.Run(async () =>
            {
                await PublicMesssage(ChangedTagDic);
            });
        }

        private async Task PublicMesssage(Dictionary<string,string> TagDic)
        {
            if (TagDic.Count()>0)
            {
                bool Result = await Client.Subscribe("data/test", TagDic);
                if (Result)
                {
                    MyDelegate.dShowProcessMessage("抛送MQTT成功！" + DateTime.Now.ToString());
                    ChangedTagDic.Clear();
                }
                else
                {
                    MyDelegate.dShowProcessMessage("抛送MQTT失败！" + DateTime.Now.ToString());
                }
            }
            
        }

        public void GetBoxInfo()
        {
            CancellationTokenSource CTS = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            List<FBoxDeviceItem> BoxList = new List<FBoxDeviceItem>();
            BoxList.Clear();
            IList<BoxGroup> GroupList = null;
            var Task1 = FBOX.GetBoxGroups();
            var Task2 = Task.Run(async () =>
            {
                try
                {
                    await Task1;
                    GroupList = Task1.Result;
                    foreach (var Group in GroupList)
                    {
                        foreach (var Box in Group.Boxes)
                        {
                            BoxList.Add(new FBoxDeviceItem
                            {
                                GroupName = Group.Name,
                                BoxName = Box.Alias,
                                BoxNO = Box.BoxNo,
                                BoxState = Box.ConnectionState.ToString()
                            });
                        }
                        MyDelegate.dGetBoxInfo(BoxList);
                    }
                }
                catch (Exception EX)
                {

                }
            },CTS.Token);
        }

        public void GetTagInfo(string BoxNum)
        {
            IList<BoxDMonGroupsDtoV2> TagGroupList;
            TagList.Clear();
            var Task1 = FBOX.GetDmonGroupDmonsV2(new BoxArgs(BoxNum));

            var Task2= Task.Run(async () =>
                {
                    try
                    {
                        await FBOX.StartAllDataMonitorPointsOnBox(new BoxArgs(BoxNum));
                        MyDelegate.dShowProcessMessage("该盒子所有点开启成功！");
                    }
                    catch (Exception e)
                    {
                        MyDelegate.dShowProcessMessage("该盒子所有点开启失败！");
                    }
                });
            Task[] Tasks = new Task[] { Task2 };

            Task.Run(async () =>
            {
                try
                {
                    Task.WaitAny(Tasks);
                    await Task1;
                    TagGroupList = Task1.Result;
                    
                    foreach (var Group in TagGroupList)
                    {
                        foreach (var Tag in Group.Items)
                        {
                            TagDic.Clear();
                            FBoxTagInfo NewTag = new FBoxTagInfo();
                            NewTag.GroupName = Group.Name;
                            NewTag.TagName = Tag.Name;
                            NewTag.TagMainAddress = Tag.MainAddress.ToString();
                            TagList.Add(NewTag);
                            TagDic.Add(Tag.Id.ToString(), NewTag);
                        }
                    }
                    MyDelegate.dGetTagInfo(TagList);

                }
                catch (Exception EX)
                {

                }
            });
        }

    }
}
