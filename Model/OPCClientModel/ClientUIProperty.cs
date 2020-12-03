using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model.OPCClientModel
{
    public class ClientUIProperty : INotifyPropertyChanged
    {
        public ClientUIProperty()
        {
            DeviceChildren = new List<DeviceProperty>();
        }

        private string _SelectedServerNode;
        public string SelectedServerNode
        {
            get { return _SelectedServerNode; }
            set
            {
                _SelectedServerNode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedServerNode"));
            }
        }

        private string _SelectedServerName;
        public string SelectedServerName
        {
            get { return _SelectedServerName; }
            set
            {
                _SelectedServerName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedServerName"));
            }
        }

        private string _SelectedTag;
        public string SelectedTag
        {
            get { return _SelectedTag; }
            set
            {
                _SelectedTag = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedTag"));
            }
        }

        private List<string> _NameList;
        public List<string> NameList
        {
            get { return _NameList; }
            set {
                _NameList = new List<string>();
                _NameList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NameList"));
            }
        }

        private string _TagName;
        public string TagName
        {
            get { return _TagName; }
            set
            {
                _TagName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TagName"));
            }
        }

        private string _Value;
        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Value"));
            }
        }

        private string _UpdateTime;
        public string UpdateTime
        {
            get { return _UpdateTime; }
            set
            {
                _UpdateTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UpdateTime"));
            }
        }

        private List<DeviceProperty> _DeviceChildren;
        public List<DeviceProperty> DeviceChildren
        {
            get { return _DeviceChildren; }
            set
            {
                _DeviceChildren = new List<DeviceProperty>();
                _DeviceChildren = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeviceChildren"));
            }
        }

        #region 属性变化通知事件

                /// <summary>
                /// 属性变化通知事件
                /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性变化通知
        /// </summary>
        /// <param name="e"></param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}
