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
using System.Windows.Shapes;
using ZdfFlatUI;
using Model.OTSM;
using BLL.OTSC;

namespace OTS.OTSV
{
    /// <summary>
    /// OTSMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OTSMainWindow : DazzleWindow
    {
        OTSConnectPage ConnectPage;
        OTSMainPage MainPage;
        OTSSettingPage SettingPage;
        OTSSettingPage2 SettingPage2;
        TableTypeSelectWindow TypeSelectWindow;
        OTSUIProperty UIProperty;
        OTSDataInfo DataInfo;
        GetServer OPCClient;
        SqlHelper SqlClient;
        PairWindow PairPage;
        SettingFactory SettingFactory;
        InitHandle InitObject;
        System.Windows.Threading.DispatcherTimer CheckNormalTimer = new System.Windows.Threading.DispatcherTimer();
        public OTSMainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            UIProperty = new OTSUIProperty();
            this.DataContext = UIProperty;
            DataInfo = new OTSDataInfo();
            OPCClient = new GetServer();
            SqlClient = new SqlHelper();
            ConnectPage = new OTSConnectPage(UIProperty,OPCClient,SqlClient);
            MainPage = new OTSMainPage(UIProperty, OPCClient, SqlClient,DataInfo);
            SettingFactory = new SettingFactory(UIProperty, OPCClient, SqlClient, DataInfo);
            TypeSelectWindow = new TableTypeSelectWindow(UIProperty, SettingFactory);
            InitObject = new InitHandle(UIProperty,DataInfo);
            InitObject.ReadConfig();
            this.ControlPanel.Content = MainPage;
            MyDelegate.dShowSettingPage += ShowSettingPage;
            MyDelegate.dShowMainPage += ShowMainPage;
            MyDelegate.dSetLedState += SetStateLed;
            MyDelegate.dShowPairWindow += PairWindowShow;
            MyDelegate.dShowMessageBox += MessageBoxShow;
            MyDelegate.dShowTypeWindow += TypeWindowShow;

        }

        private void ClickSetting(object sender, RoutedEventArgs e)
        {
            if (UIProperty.ConnectState == "未连接")
            {
                this.ControlPanel.Content = ConnectPage;
            }
            if (UIProperty.ConnectState == "已连接")
            {
                switch (UIProperty.SelectTableType)
                {
                    case "横向表":
                        this.ControlPanel.Content = SettingPage;
                        break;
                    case "纵向表":
                        this.ControlPanel.Content = SettingPage2;
                        break;
                }
            }
        }

        public void ShowSettingPage(string Type, SettingVirClass SettingObject)
        {
            switch (Type) {
                case "横向表":
                    SettingPage = new OTSSettingPage(SettingObject);
                    this.ControlPanel.Content = SettingPage;
                    break;
                case "纵向表":
                    SettingPage2 = new OTSSettingPage2(SettingObject);
                    this.ControlPanel.Content = SettingPage2;
                    break;
            }
        }

        public void ShowMainPage()
        {
            this.ControlPanel.Content = MainPage;
        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            MyDelegate.dWriteToSaveFile?.Invoke();
            System.Diagnostics.Process RunProcess = System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id);
            RunProcess.Kill();
        }

        private void ClickMax(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void ClickMainPage(object sender, RoutedEventArgs e)
        {
            this.ControlPanel.Content = MainPage;
            
        }

        private void ClickDisConnect(object sender, RoutedEventArgs e)
        {
            if (OPCClient.DisConnect() && SqlClient.SqlClose())
            {
                MessageBox.Show("连接已关闭！");
                SetStateLed(false);
                MyDelegate.dSetConnectText(false);
            }
            else
            {
                MessageBox.Show("连接未成功关闭！请先连接！");
            }
        }

        private void SetStateLed(bool State)
        {
            switch (State)
            {
                case true:
                    StateLed.Fill = Brushes.Green;
                    UIProperty.ConnectState = "已连接";
                    break;
                case false:
                    StateLed.Fill = Brushes.Red;
                    UIProperty.ConnectState = "未连接";
                    break;
            }
                
        }

        private void PairWindowShow()
        {
            PairPage = new PairWindow(UIProperty, DataInfo);
            PairPage.Owner = this;
            PairPage.ShowDialog();
        }

        private void TypeWindowShow()
        {
            TypeSelectWindow = new TableTypeSelectWindow(UIProperty, SettingFactory);
            TypeSelectWindow.Owner = this;
            TypeSelectWindow.ShowDialog();
        }

        public void MessageBoxShow(string Message)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(Message);
            }));
        }
    }
}
