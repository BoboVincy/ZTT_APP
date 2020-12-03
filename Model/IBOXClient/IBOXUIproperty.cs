using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UIPropertyModel;

namespace Model.IBOXClient
{
     public class IBOXUIproperty:UIProperty   
    {
        public IBOXUIproperty()
        {
            ClientID = "fbclient1";
            ClientKey = "28911fd00ce743e1a7301c27a319618b";
            UserName = "device_cloud";
            PassWord = "Ztt12345.";
            IDServer = "https://account.zttiot.net/core/";
            APPServer = "https://app.zttiot.net";
            HDataServer = "https://hs.zttiot.net";
        }
        private string _ClientID;
        public string ClientID
        {
            get
            {
                return _ClientID;
            }

            set
            {
                _ClientID = value;
                OnPropertyChanged("ClientID");
            }
        }

        private string _ClientKey;
        public string ClientKey
        {
            get
            {
                return _ClientKey;
            }

            set
            {
                _ClientKey = value;
                OnPropertyChanged("ClientKey");
            }
        }

        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _PassWord;
        public string PassWord
        {
            get
            {
                return _PassWord;
            }

            set
            {
                _PassWord = value;
                OnPropertyChanged("PassWord");
            }
        }

        private string _StateMessage;
        public string StateMessage
        {
            get
            {
                return _StateMessage;
            }

            set
            {
                _StateMessage = value;
                OnPropertyChanged("StateMessage");
            }
        }

        private string _IDServer;
        public string IDServer
        {
            get
            {
                return _IDServer;
            }

            set
            {
                _IDServer = value;
                OnPropertyChanged("IDServer");
            }
        } 
        private string _APPServer;
        public string APPServer
        {
            get
            {
                return _APPServer;
            }

            set
            {
                _APPServer = value;
                OnPropertyChanged("APPServer");
            }
        } 

        private string _HDataServer;
        public string HDataServer
        {
            get
            {
                return _HDataServer;
            }

            set
            {
                _HDataServer = value;
                OnPropertyChanged("HDataServer");
            }
        } 

        private string _MQTTHost;
        public string MQTTHost
        {
            get
            {
                return _MQTTHost;
            }

            set
            {
                _MQTTHost = value;
                OnPropertyChanged("MQTTHost");
            }
        }

        private string _MQTTPort;
        public string MQTTPort
        {
            get
            {
                return _MQTTPort;
            }

            set
            {
                _MQTTPort = value;
                OnPropertyChanged("MQTTPort");
            }
        }

        private string _MQTTUserName;
        public string MQTTUserName
        {
            get
            {
                return _MQTTUserName;
            }

            set
            {
                _MQTTUserName = value;
                OnPropertyChanged("MQTTUserName");
            }
        }

        private string _MQTTUserPWD;
        public string MQTTUserPWD
        {
            get
            {
                return _MQTTUserPWD;
            }

            set
            {
                _MQTTUserPWD = value;
                OnPropertyChanged("MQTTUserPWD");
            }
        }

        private string _SelectedDevice;
        public string SelectedDevice
        {
            get
            {
                return _SelectedDevice;
            }

            set
            {
                _SelectedDevice = value;
                OnPropertyChanged("SelectedDevice");
            }
        }
        private string _GroupName;
        public string GroupName
        {
            get
            {
                return _GroupName;
            }

            set
            {
                _GroupName = value;
                OnPropertyChanged("GroupName");
            }
        }

        private string _TagName;
        public string TagName
        {
            get
            {
                return _TagName;
            }

            set
            {
                _TagName = value;
                OnPropertyChanged("TagName");
            }
        }

        private string _TagAddress;
        public string TagAddress
        {
            get
            {
                return _TagAddress;
            }

            set
            {
                _TagAddress = value;
                OnPropertyChanged("TagAddress");
            }
        }

        private string _TagValue;
        public string TagValue
        {
            get
            {
                return _TagValue;
            }

            set
            {
                _TagValue = value;
                OnPropertyChanged("TagValue");
            }
        }

    }
}
