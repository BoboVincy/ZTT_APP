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
using System.IO;

namespace Common.MQTT
{
    public class MQTTCommon
    {
        public class LoginModel
        {
            public string Host
            {
                get; set;
            }

            public int Port
            {
                get; set;
            }

            public string UserName
            {
                get; set;
            }

            public string PassWord
            {
                get; set;
            }

            public string ClientID
            {
                get; set;
            }

            public bool ClenSession
            {
                get; set;
            } = true;

            public ushort KeepAlived
            {
                get; set;
            } = 120;
        }

        public LoginModel Param;
        public IMqttClient Client;
        private MqttClientOptions Option;
        public MQTTCommon(LoginModel Param)
        {
            this.Param = Param;
        }

        public async void MQTTConnect()
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
            }
            catch (Exception EX)
            {

            }
            

            

        }
    }
}
