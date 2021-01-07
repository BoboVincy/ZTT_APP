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
        public List<string> ChangedTagList;
        IBOXMQTT Client;
        System.Windows.Threading.DispatcherTimer ReadDataTimer = new System.Windows.Threading.DispatcherTimer();
        private static IFBoxClientManager FBOX;
        private static Dictionary<string, FBoxTagInfo> TagDic = new Dictionary<string, FBoxTagInfo>();
        List<FBoxTagInfo> TagList = new List<FBoxTagInfo>();
        IList<string> Names = new List<string>();
        IList<long> Ids = new List<long>();
        IList<Tuple<string, string>> NamesWithGroup = new List<Tuple<string, string>>();



        public GetIBOXServer(string ClientID,string ClientKey,string UserName,string PassWord,string IDServer,string APPServer,string HDataServer)
        {
            this.ClientID = ClientID;
            this.ClientKey = ClientKey;
            this.UserName = UserName;
            this.PassWord = PassWord;
            this.IDServer = IDServer;
            this.APPServer = APPServer;
            this.HDataServer = HDataServer;
            ChangedTagList = new List<string>(); 
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
                    ChangedTagList.Add("{\"TagName\":\"" + TagItem.Name+ "\",\"TagValue\":\"" + TagItem.Value.ToString()+ "\",\"InsertTime\":\"" + DateTime.Now.ToString()+"\"}");
                }
                MyDelegate.dGetTagInfo(TagList);

            }
        }

        private void ToMQTT(object Sender, EventArgs E)
        {
            Task.Run(async () =>
            {
                await PublicMesssage(ChangedTagList);
            });
        }

        private async Task PublicMesssage(List<string> TagList)
        {
            if (ChangedTagList.Count()>0)
            {
                bool Result = await Client.Subscribe("data/test", TagList);
                if (Result)
                {
                    MyDelegate.dShowProcessMessage("抛送MQTT成功！" + DateTime.Now.ToString());
                    ChangedTagList.Clear();
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


        public async Task GetTag(string BoxNum)
        {
            var Task1 = FBOX.StartAllDataMonitorPointsOnBox(new BoxArgs(BoxNum));
            try
            {
                await Task1;
                
            }
            catch (Exception EX)
            {

            }
        }

        public async Task GetTagValue(string BoxNum)
        {
            IList<BoxDMonGroupsDtoV2> TagGroupList;
            NamesWithGroup = new List<Tuple<string, string>>();
            Ids = new List<long>();
            Names = new List<string>();
            var Task1 = FBOX.StartAllDataMonitorPointsOnBox(new BoxArgs(BoxNum));
            var Task2 = FBOX.GetDmonGroupDmonsV2(new BoxArgs(BoxNum));
            try
            {
                await Task1;
                IList<DMonEntry> TagValueList;
                Task1.Wait();
                await Task2;
                TagGroupList = Task2.Result;
                foreach (var Group in TagGroupList)
                {
                    foreach (var Tag in Group.Items)
                    {
                        FBoxTagInfo NewTag = new FBoxTagInfo();
                        NewTag.GroupName = Group.Name;
                        NewTag.TagName = Tag.Name;
                        NewTag.TagMainAddress = Tag.MainAddress.ToString();
                        TagDic.Add(Tag.Id.ToString(), NewTag);
                        var Tuple1 = Tuple.Create<string, string>(Group.Name, Tag.Name);
                        NamesWithGroup.Add(Tuple1);
                        Names.Add(Tag.Name);
                        Ids.Add(Tag.Id);
                        TagList.Add(NewTag);
                    }
                }
                Task2.Wait();
                GetDMonValueArgs ValueArgs = new GetDMonValueArgs { BoxNo = BoxNum, Names = Names, NamesWithGroup = NamesWithGroup, Ids = Ids, UseCache = true, Timeout = 5000 };
                var Task3 = FBOX.GetDMonValue(ValueArgs);

                await Task3;
                TagValueList = Task3.Result;
                foreach (var Item in TagValueList)
                {
                    TagDic[Item.Id.ToString()].TagValue = Item.Value.ToString();
                }
                MyDelegate.dGetTagInfo(TagList);
            }
            catch (Exception EX)
            {

            }
        }

        public void GetTagInfo(string BoxNum)
        {
            IList<BoxDMonGroupsDtoV2> TagGroupList;
            IList<DMonEntry> TagValueList;
            TagList.Clear();
            var Task1 = FBOX.GetDmonGroupDmonsV2(new BoxArgs(BoxNum));
            Task.Run(async () =>
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
            Task.Run(async () =>
            {
                try
                {
                    await Task1;
                    TagGroupList = Task1.Result;
                    IList<Tuple<string, string>> NamesWithGroup = new List<Tuple<string, string>>();
                    IList<string> Names = new List<string>();
                    IList<long> Ids = new List<long>();
                    var Task3 = FBOX.GetDMonValue(new GetDMonValueArgs { BoxNo = BoxNum, Names = Names, NamesWithGroup = NamesWithGroup, Ids = Ids, UseCache = true });
                    foreach (var Group in TagGroupList)
                    {
                        foreach (var Tag in Group.Items)
                        {
                            FBoxTagInfo NewTag = new FBoxTagInfo();
                            NewTag.GroupName = Group.Name;
                            NewTag.TagName = Tag.Name;
                            NewTag.TagMainAddress = Tag.MainAddress.ToString();
                            TagDic.Add(Tag.Id.ToString(), NewTag);
                            var Tuple1 = Tuple.Create<string, string>(Group.Name, Tag.Name);
                            NamesWithGroup.Add(Tuple1);
                            Names.Add(Tag.Name);
                            Ids.Add(Tag.Id);
                            TagList.Add(NewTag);
                        }
                    }
                    await Task3;
                    TagValueList = Task3.Result;
                    foreach (var Item in TagValueList)
                    {
                        TagDic[Item.Id.ToString()].TagValue = Item.Value.ToString();
                    }
                    MyDelegate.dGetTagInfo(TagList);
                    FBOX.DataMonitorValueChanged += FBOXTagChanged;
                }
                catch (Exception EX)
                {

                }
            });
        }

    }
}
