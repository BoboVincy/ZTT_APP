using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.OTSC;
using Model.OTSM;
using System.Data;

namespace OTS.OTSV
{
    /// <summary>
    /// OTSSettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class OTSConnectPage : UserControl
    {
        bool KepServerFlag = false;
        bool DataBaseFlag = false;
        public MornitorBLL<bool> MorniterConnectState = null;
        GetServer OPCClient;
        SqlHelper SqlClient;
        OTSUIProperty UIProperty;
        List<string> HostNameList;
        public OTSConnectPage(OTSUIProperty UIProperty, GetServer OPCClient, SqlHelper SqlClient)
        {
            InitializeComponent();
            this.UIProperty = UIProperty;
            this.OPCClient = OPCClient;
            this.SqlClient = SqlClient;
            Init();
        }
        public void Init()
        {
            MorniterConnectState = new MornitorBLL<bool>();
            MorniterConnectState.OnMyValueEqualed += GetConnect;
            MyDelegate.dSetConnectText += DisConnectState;
        }

        private void ClickKepServerConnect(object sender, RoutedEventArgs e)
        {
            KepServerFlag = false;
            HostNameList = new List<string>();
            List<string> ServerNameList = new List<string>();
            HostNameList = OPCClient.GetHostName(UIProperty.OPCServerIP);
            if (HostNameList.Count > 0)
            {
                foreach (string HostName in HostNameList)
                {
                    foreach (string Item in OPCClient.GetServerNode(HostName))
                    {
                        ServerNameList.Add(Item);
                    }
                    if (ServerNameList.Count > 0)
                    {
                        break;
                    }
                }
            }
            if (ServerNameList.Count > 0)
            {
                UIProperty.KepServerNodeList = ServerNameList;
                this.KepServerState.Text = "已连接";
                this.KepServerState.Foreground = Brushes.Green;
                UIProperty.KepServerStateText = UIProperty.OPCServerIP;
                KepServerFlag = true;
            }
            else
            {
                this.KepServerState.Text = "未连接";
                MessageBox.Show("连接失败！");
            }
            MorniterConnectState.ValueEqual(KepServerFlag, DataBaseFlag,true);
        }

        private void ClickDataBaseConnect(object sender, RoutedEventArgs e)
        {
            DataBaseFlag = false;
            bool Result = SqlClient.SqlOpen(UIProperty.DataBaseIP, UIProperty.DataBaseUserName, UIProperty.DataBasePassWord, UIProperty.DataBaseName);
            if (Result == true)
            {
                List<string>TableList = SqlClient.GetSqlResult("select * from sysobjects where xtype='U'");
                if (TableList != null)
                {
                    UIProperty.DataTableList = TableList;
                    this.DataBaseState.Text = "已连接";
                    this.DataBaseState.Foreground = Brushes.Green;
                    UIProperty.DataBaseStateText = UIProperty.DataBaseIP;
                    DataBaseFlag = true;
                }
                else
                {
                    this.DataBaseState.Text = "数据库中没有表";
                }
                
            }
            else
            {
                this.DataBaseState.Text = "未连接";
                MessageBox.Show("连接失败！");
            }
            MorniterConnectState.ValueEqual(KepServerFlag, DataBaseFlag,true);
        }

        public void GetConnect(object Sender, EventArgs E)
        {
            MyDelegate.dSetLedState(true);
            MyDelegate.dShowTypeWindow() ;
        }

        private void DisConnectState(bool State)
        {
            if (!State)
            {
                this.DataBaseState.Text = "已断开";
                this.DataBaseState.Foreground = Brushes.Red;
                this.KepServerState.Text = "已断开";
                this.KepServerState.Foreground = Brushes.Red;
                KepServerFlag = false;
                DataBaseFlag = false;
            }
        }
    }
}
