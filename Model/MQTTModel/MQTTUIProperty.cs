using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UIPropertyModel;

namespace Model.MQTTModel
{
    public class MQTTUIProperty:UIProperty
    {
        public MQTTUIProperty()
        {
            Host = "127.0.0.1";
            Port = "11883";
            UserName = "admin";
            PassWord = "public";
        }

        private string _Host;
        public string Host
        {
            get {
                return _Host;
            }
            set {
                _Host = value;
                OnPropertyChanged("Host");
            }
        }

        private string _Port;
        public string Port
        {
            get {
                return _Port;
            }
            set {
                _Port = value;
                OnPropertyChanged("Port");
            }
        }

        private string _UserName;
        public string UserName
        {
            get {
                return _UserName;
            }
            set {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _PassWord;
        public string PassWord
        {
            get {
                return _PassWord;
            }
            set {
                _PassWord = value;
                OnPropertyChanged("PassWord");
            }
        }

        private string _Topic;
        public string Topic
        {
            get {
                return _Topic;
            }
            set {
                _Topic = value;
                OnPropertyChanged("Topic");
            }
        }
    }
}
