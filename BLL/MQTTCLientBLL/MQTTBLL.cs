using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.MQTT.Model;
using Common.MQTT.BLL;

namespace BLL.MQTTCLientBLL
{
    
    public class MQTTBLL
    {
        LoginModel LoginParam;
        MQTTCommon Client;
        public MQTTBLL(LoginModel LoginParam)
        {
            this.LoginParam = LoginParam;
            Client = new MQTTCommon(LoginParam);
        }

        public async Task Connect()
        {
            try
            {
                bool Result = await Client.MQTTConnect();
                
            }
            catch(Exception EX) {
            }
        }
    }
}
