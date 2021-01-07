using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MQTT.Model
{
    public class LoginModel
    {
        public string Host
        {
            get;set;
        }

        public int Port
        {
            get;set;
        }

        public string UserName
        {
            get;set;
        }

        public string PassWord
        {
            get;set;
        }

        public string ClientID
        {
            get;set;
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
}
