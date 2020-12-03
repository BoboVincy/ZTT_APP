using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.MQTT.BLL;
using Common.MQTT.Model;
using Common.Extension;

namespace BLL.IBOXClientBLL
{
    public class IBOXMQTT
    {
        private LoginModel LoginParam;
        private MQTTCommon Client;
        public IBOXMQTT(LoginModel LoginParam)
        {
            this.LoginParam = LoginParam;
            Client = new MQTTCommon(LoginParam);
        }

        public async Task<bool> Subscribe(string Topic, Dictionary<string,string> TagDic)
        {
            try
            {
                string JsonContent= TagDic.ToJson();
                bool Result = await Client.MQTTConnect();
                if (Result)
                {
                    bool Result1 = await Client.MQTTPublish(Topic, JsonContent);
                    if (Result1)
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
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
