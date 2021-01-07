using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UIPropertyModel;

namespace Model.ModbusModel
{

    public class ModbusUIProperty : UIProperty
    {
        public string Modbustype
        {
            get;set;
        }

        private string _BaudRate;
        public string BaudRate
        {
            get {
                return _BaudRate;
            }
            set {
                _BaudRate = value;
                OnPropertyChanged("BaudRate");
            }
        }

        private string _DataBit;
        public string DataBit
        {
            get {
                return _DataBit;
            }
            set {
                _DataBit = value;
                OnPropertyChanged("DataBit");
            }
        }

        private string _Parity;
        public string Parity
        {
            get {
                return _Parity;
            }
            set
            {
                _Parity = value;
                OnPropertyChanged("Parity");
            }
        }

        private string _StopBits;
        public string StopBits
        {
            get
            {
                return _StopBits;
            }
            set
            {
                _StopBits = value;
                OnPropertyChanged("StopBits");
            }
        }

        private string _PortName;
        public string PortName
        {
            get
            {
                return _PortName;
            }
            set
            {
                _PortName = value;
                OnPropertyChanged("PortName");
            }
        }

        private string _DeviceNum;
        public string DeviceNum
        {
            get
            {
                return _DeviceNum;
            }
            set
            {
                _DeviceNum = value;
                OnPropertyChanged("DeviceNum");
            }
        }

        private string _RegisterType;
        public string RegisterType
        {
            get
            {
                return _RegisterType;
            }
            set
            {
                _RegisterType = value;
                OnPropertyChanged("RegisterType");
            }
        }

        private string _StartBit;
        public string StartBit
        {
            get
            {
                return _StartBit;
            }
            set
            {
                _StartBit = value;
                OnPropertyChanged("StartBit");
            }
        }

        private string _Length;
        public string Length
        {
            get
            {
                return _Length;
            }
            set
            {
                _Length = value;
                OnPropertyChanged("Length");
            }
        }

        private string _Register;
        public string Register
        {
            get
            {
                return _Register;
            }
            set
            {
                _Register = value;
                OnPropertyChanged("Register");
            }
        }

        private string _RegisterValue;
        public string RegisterValue
        {
            get
            {
                return _RegisterValue;
            }
            set
            {
                _RegisterValue = value;
                OnPropertyChanged("RegisterValue");
            }
        }

        private string _TCPIP;
        public string TCPIP
        {
            get
            {
                return _TCPIP;
            }
            set
            {
                _TCPIP = value;
                OnPropertyChanged("TCPIP");
            }
        }

        private string _TCPPort;
        public string TCPPort
        {
            get
            {
                return _TCPPort;
            }
            set
            {
                _TCPPort = value;
                OnPropertyChanged("TCPPort");
            }
        }

    }
}
