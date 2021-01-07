using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using System.IO;
using Common.MQTT.Model;
using Common.Extension;

namespace Common.MQTT.BLL
{
    public class MQTTCommon
    {

        LoginModel Param;
        public IMqttClient Client;
        private MqttClientOptions Option;
        public List<string> MessageList;
        Action<MqttApplicationMessageReceivedEventArgs> MQTTRecieve;
        public MQTTCommon(LoginModel Param)
        {
            this.Param = Param;
        }

        public async Task<bool> MQTTConnect()
        {

            try
            {
                Option = new MqttClientOptions() { ClientId = Guid.NewGuid().ToString("D") };
                Option.ChannelOptions = new MqttClientTcpOptions()
                {
                    Server = Param.Host,
                    Port = Param.Port
                };
                Option.Credentials = new MqttClientCredentials()
                {
                    Username = Param.UserName,
                    Password = System.Text.Encoding.Default.GetBytes(Param.PassWord)
                };

                Option.CleanSession = Param.ClenSession;
                Option.KeepAlivePeriod = TimeSpan.FromSeconds(100.5);


                if (Client != null)
                {
                    await Client.DisconnectAsync();

                }

                Client = new MqttFactory().CreateMqttClient();
                await Client.ConnectAsync(Option);
                MessageList = new List<string>();
                MessageList.Clear();
                MQTTRecieve = MQTTRecieveEvent;
                Client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(MQTTRecieve);
                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
        }


        public async Task<bool> MQTTSubscribe(string Topic)
        {
            try
            {
                await Client.SubscribeAsync(Topic);
                return true;
            }
            catch (Exception EX)
            {
                return false;
            }

        }

        public async Task<bool> MQTTPublish(string Topic, string Content)
        {
            try
            {
                List<MqttApplicationMessage> MQTTMessage = new List<MqttApplicationMessage>();
                MQTTMessage.Add(new MqttApplicationMessage {
                    Topic = Topic,
                    Payload = Encoding.Default.GetBytes(Content),
                    Retain = true,
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,

                });
                await Client.PublishAsync(MQTTMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void MQTTRecieveEvent(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                string Text = Encoding.Default.GetString(e.ApplicationMessage.Payload);
                DataInsert(DataHandle(Text));
                MessageList.Add("来自" + e.ApplicationMessage.Topic + "的消息：" + Text + " " + DateTime.Now.ToString());
            }
            catch (Exception E)
            {

            }
        }




        //private List<SqlInfo> DataHandle(string JsonContent)
        //{
        //    List<SqlInfo> Result = new List<SqlInfo>();
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    Result = js.Deserialize<List<SqlInfo>>(JsonContent);
        //    return Result;
        //}

        public static bool DataInsert(List<SqlInfo> DataList)
        {
            bool OpenResult = SqlHelper.SqlOpen("server=127.0.0.1;uid=admin;pwd=chen3724913416;DataBase=Test");
            if (OpenResult == true)
            {
                bool ModifyResult = false;
                foreach (SqlInfo Item in DataList)
                {
                    ModifyResult = SqlHelper.SqlModify(string.Format(@"insert into Test.dbo.MQTTTest(TagName,TagValue,InsertTime) values('{0}',{1},'{2}')", Item.TagName, Item.TagValue, Item.InserTime));
                    if (ModifyResult == false)
                    {
                        break;

                    }
                }
                if (ModifyResult == true)
                {
                    if (SqlHelper.SqlClose())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private List<SqlInfo> DataHandle(string JsonContent)
        {
            List<string> Result = new List<string>();
            List<string> DataList = new List<string>();
            List<SqlInfo> SqlList = new List<SqlInfo>();
            Result = JsonHelper.DeserializeJsonToList(JsonContent);
            foreach (var Item in Result)
            {
                var ItemDic = Item.ToJsonReader();
                
                SqlList.Add(new SqlInfo
                {
                    TagName = ItemDic["TagName"].ToString(),
                    TagValue = ItemDic["TagValue"].ToString(),
                    InserTime = DateTime.Now
                });
            }
            return SqlList;


        }
    }
}
