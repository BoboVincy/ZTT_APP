using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model.OTSM
{
    public class OTSUIProperty : INotifyPropertyChanged
    {
        public OTSUIProperty()
        {
            TableTypeList = new List<string>() { "横向表", "纵向表" };
            FiledDescriptionList = new List<string>() { "点名称", "点值" };
            WriteFileNum = 20;
            ProcExeTime = 20;
            //OPCServerIP = "localhost";
            //DataBaseIP = "localhost";
            //DataBaseUserName = "admin";
            //DataBasePassWord = "chen3724913416";
            //DataBaseName = "Test";
            //ExcelPath = "C:/Users/15625/Documents/WeChat Files/wxid_pzvchu67l3aq22/FileStorage/File/2020-12/数据表结构最新（添加单位）.xlsx";
        }

        /// <summary>
        /// 选择的服务端节点
        /// </summary>
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
        /// <summary>
        /// 选择的表
        /// </summary>
        private string _SelectedTable;
        public string SelectedTable
        {
            get { return _SelectedTable; }
            set
            {
                _SelectedTable = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedTable"));
            }
        }
        /// <summary>
        /// OPC服务端IP
        /// </summary>
        private string _OPCServerIP;
        public string OPCServerIP
        {
            get { return _OPCServerIP; }
            set
            {
                _OPCServerIP = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OPCServerIP"));
            }
        }
        /// <summary>
        /// 数据库IP
        /// </summary>
        private string _DataBaseIP;
        public string DataBaseIP
        {
            get { return _DataBaseIP; }
            set
            {
                _DataBaseIP = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataBaseIP"));
            }
        }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        private string _DataBaseUserName;
        public string DataBaseUserName
        {
            get { return _DataBaseUserName; }
            set
            {
                _DataBaseUserName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataBaseUserName"));
            }
        }
        /// <summary>
        /// 数据库密码
        /// </summary>
        private string _DataBasePassWord;
        public string DataBasePassWord
        {
            get { return _DataBasePassWord; }
            set
            {
                _DataBasePassWord = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataBasePassWord"));
            }
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        private string _DataBaseName;
        public string DataBaseName
        {
            get { return _DataBaseName; }
            set
            {
                _DataBaseName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataBaseName"));
            }
        }
        /// <summary>
        /// KepServer服务端连接状态
        /// </summary>
        private string _KepServerStateText;
        public string KepServerStateText
        {
            get { return _KepServerStateText; }
            set
            {
                _KepServerStateText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("KepServerStateText"));
            }
        }
        /// <summary>
        /// 数据库连接状态
        /// </summary>
        private string _DataBaseStateText;
        public string DataBaseStateText
        {
            get { return _DataBaseStateText; }
            set
            {
                _DataBaseStateText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataBaseStateText"));
            }
        }
        /// <summary>
        /// 选择的点
        /// </summary>
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
        /// <summary>
        /// 选择的频道
        /// </summary>
        private string _SelectedChannel;
        public string SelectedChannel
        {
            get { return _SelectedChannel; }
            set
            {
                _SelectedChannel = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedChannel"));
            }
        }
        /// <summary>
        /// 选择的设备
        /// </summary>
        private string _SelectedDevice;
        public string SelectedDevice
        {
            get { return _SelectedDevice; }
            set
            {
                _SelectedDevice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedDevice"));
            }
        }
        /// <summary>
        /// 选择的字段
        /// </summary>
        private string _SelectedField;
        public string SelectedField
        {
            get { return _SelectedField; }
            set
            {
                _SelectedField = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedField"));
            }
        }
        /// <summary>
        /// 点名称列表
        /// </summary>
        private List<string> _TagNameList;
        public List<string> TagNameList
        {
            get { return _TagNameList; }
            set
            {
                _TagNameList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TagNameList"));
            }
        }
        /// <summary>
        /// 字段名称列表
        /// </summary>
        private List<string> _FieldNameList;
        public List<string> FieldNameList
        {
            get { return _FieldNameList; }
            set
            {
                _FieldNameList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FieldNameList"));
            }
        }
        /// <summary>
        /// KepServer节点列表
        /// </summary>
        private List<string> _KepServerNodeList;
        public List<string> KepServerNodeList
        {
            get { return _KepServerNodeList; }
            set {
                _KepServerNodeList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("KepServerNodeList"));
            }
        }
        /// <summary>
        /// 数据表列表
        /// </summary>
        private List<string> _DataTableList;
        public List<string> DataTableList
        {
            get { return _DataTableList; }
            set
            {
                _DataTableList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataTableList"));
            }
        }
        /// <summary>
        /// 频道列表
        /// </summary>
        private List<string> _ChannelList;
        public List<string> ChannelList
        {
            get { return _ChannelList; }
            set
            {
                _ChannelList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ChannelList"));
            }
        }
        /// <summary>
        /// 设备列表
        /// </summary>
        private List<string> _DeviceList;
        public List<string> DeviceList
        {
            get { return _DeviceList; }
            set
            {
                _DeviceList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeviceList"));
            }
        }
        /// <summary>
        /// 该表匹配的字段值列表
        /// </summary>
        private List<string> _ShowItemList;
        public List<string> ShowItemList
        {
            get { return _ShowItemList; }
            set
            {
                _ShowItemList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ShowItemList"));
            }
        }
        /// <summary>
        /// 所有表的字段值列表
        /// </summary>
        private List<string> _AllItemList;
        public List<string> AllItemList
        {
            get { return _AllItemList; }
            set
            {
                _AllItemList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AllItemList"));
            }
        }
        /// <summary>
        /// OPC服务端跟KepServer连接状态
        /// </summary>
        private string _ConnectState = "未连接";
        public string ConnectState
        {
            get { return _ConnectState; }
            set
            {
                _ConnectState = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ConnectState"));
            }
        }
        /// <summary>
        /// Excel数据表状态
        /// </summary>
        private string _ExcelPath;
        public string ExcelPath
        {
            get { return _ExcelPath; }
            set
            {
                _ExcelPath = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ExcelPath"));
            }
        }
        /// <summary>
        /// 采集间隔时间
        /// </summary>
        private string _IntervalTime;
        public string IntervalTime
        {
            get { return _IntervalTime; }
            set
            {
                _IntervalTime = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IntervalTime"));
            }
        }

        private List<string> _TableTypeList;
        public List<string> TableTypeList
        {
            get { return _TableTypeList; }
            set
            {
                _TableTypeList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TableTypeList"));
            }
        }

        private string _SelectTableType;
        public string SelectTableType
        {
            get { return _SelectTableType; }
            set
            {
                _SelectTableType = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectTableType"));
            }
        }



        private List<string> _FiledDescriptionList;
        public List<string> FiledDescriptionList
        {
            get { return _FiledDescriptionList; }
            set
            {
                _FiledDescriptionList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FiledDescriptionList"));
            }
        }

        private List<string> _DescriptionByFieldList;
        public List<string> DescriptionByFieldList
        {
            get { return _DescriptionByFieldList; }
            set
            {
                _DescriptionByFieldList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DescriptionByFieldList"));
            }
        }

        private string _SelectedDescription;
        public string SelectedDescription
        {
            get { return _SelectedDescription; }
            set
            {
                _SelectedDescription = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedDescription"));
            }
        }

        private string _SelectedView;
        public string SelectedView
        {
            get { return _SelectedView; }
            set
            {
                _SelectedView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedView"));
            }
        }

        private string _SelectedTypeDescription;
        public string SelectedTypeDescription
        {
            get { return _SelectedTypeDescription; }
            set
            {
                _SelectedTypeDescription = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedTypeDescription"));
            }
        }

        private string _TableName;
        public string TableName
        {
            get { return _TableName; }
            set
            {
                _TableName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TableName"));
            }
        }

        private string _FieldName;
        public string FieldName
        {
            get { return _FieldName; }
            set
            {
                _FieldName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FieldName"));
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


        /// <summary>
        /// 采集状态
        /// </summary>
        public bool GatherState
        {
            get; set;
        }

        public string WillDeleteItem
        {
            get;set;
        }

        public int WriteFileNum
        {
            get;set;
        }

        public int ProcExeTime
        {
            get;set;
        }

        public int CheckTime
        {
            get;set;
        }

        public string SavedTableType
        {
            get;set;
        }

        public string SavedDataBaseIP
        {
            get;set;
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
