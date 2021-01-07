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
using BLL.OTSC;
using Model.OTSM;
using Common.Log4net;
using System.Collections.ObjectModel;

namespace OTS.OTSV
{
    /// <summary>
    /// OTSMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class OTSMainPage : UserControl
    {
        GetServer OPCClient;
        SqlHelper SqlClient;
        OTSUIProperty UIProperty;
        OTSDataInfo DataInfo;
        System.Windows.Threading.DispatcherTimer ReadDataTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer XmlToBaseTimer=new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer CheckNormalTimer = new System.Windows.Threading.DispatcherTimer();
        ObservableCollection<OTSUIProperty> GridTagList = new ObservableCollection<OTSUIProperty>();
        private int TypeFlag = 3;//1:订阅 2:定时 3:未选择
        private List<string> TempDataStrList = new List<string>();


        public OTSMainPage(OTSUIProperty UIProperty, GetServer OPCClient, SqlHelper SqlClient,OTSDataInfo DataInfo)
        {
            InitializeComponent();
            this.UIProperty = UIProperty;
            this.OPCClient = OPCClient;
            this.SqlClient = SqlClient;
            this.DataInfo = DataInfo;
            //MyDelegate.dUpdateViewList += ViewList;
            MyDelegate.dShowItem += ViewList;
            XmlToBaseTimer.Tick += new EventHandler(ReadFileToDataBase);
            ReadDataTimer.Tick += new EventHandler(ReadData);
            CheckNormalTimer.Tick += new EventHandler(CheckIsNormal);
            InitialRouteEvents();
        }

        /// <summary>
        /// 查询类型按键添加路由事件
        /// </summary>
        private void InitialRouteEvents()
        {
            try
            {
                this.StartPanel.AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioChecked));
            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("查询类型按键添加路由事件出错！" + EX.Message);
            }
        }

        private void RadioChecked(object Sender,RoutedEventArgs E)
        {
            try
            {
                UIElement s = (UIElement)E.OriginalSource;
                RadioButton button = new RadioButton();
                button = s as RadioButton;
                switch (button.Name)
                {
                    case "SubscribeButton":
                        TypeFlag = 1;
                        if (ReadDataTimer.IsEnabled)
                        {
                            ReadDataTimer.Stop();
                        }
                        break;
                    case "TimerButton":
                        TypeFlag = 2;
                        MyDelegate.dSetSubscribeType(false);
                        break;
                }

            }
            catch (Exception EX)
            {
                LogHelp.Log.Error("类型按键出错！" + EX.Message);
            }
        }
        public void ViewList(List<TableFieldInfo> TagList)
        {
            //this.ShowViewList.Items.Refresh();
            UpdateGrid(TagList);
        }

        public bool CheckCanStart(string Type)
        {
            switch (Type)
            {
                case "横向表":
                    if (DataInfo.SavedBaseToServerDic != DataInfo.BaseToServerDic || DataInfo.SavedBaseToServerDic == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "纵向表":
                    if (DataInfo.SavedPortraitTableToTagListDic != DataInfo.PortraitTableToTagListDic || DataInfo.SavedPortraitTableToTagListDic == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
            }
            return false;
        }

        private void ClickGather(object sender, RoutedEventArgs e)
        {
            if (UIProperty.GatherState == true)
            {
                MessageBox.Show("程序已在运行中！");
                return;
            }
            if (!CheckCanStart(UIProperty.SelectTableType))
            {
                MessageBox.Show("之前的配置还未保存或配置为空，请先配置并保存再进行结转！");
                return;
            }

            foreach (string Table in DataInfo.GatherTableToFeildList.Keys)
            {
                FileHelper.CreateFile("C:/OTS/DataLogTempPool", Table + ".txt");
            }
            CheckNormalTimer.Interval = new TimeSpan(0, 0, UIProperty.CheckTime, 0);
            XmlToBaseTimer.Interval = new TimeSpan(0, 0, 0, UIProperty.ProcExeTime);
            switch (TypeFlag) {

                case 1:
                    if (DataInfo.GatherTagList.Count != 0)
                    {
                        OPCClient.GetTagValue(DataInfo.GatherTagList);
                    }
                    MyDelegate.dSetSubscribeType(true);
                    StateGather.Fill = Brushes.Green;
                    UIProperty.GatherState = true;
                    break;

                case 2:
                    if (UIProperty.IntervalTime != "" && UIProperty.IntervalTime != null)
                    {
                        try
                        {
                            ReadDataTimer.Interval = new TimeSpan(0,0,0,0,Convert.ToInt32(UIProperty.IntervalTime));
                            if (DataInfo.GatherTagList.Count != 0)
                            {
                                OPCClient.GetTagValue(DataInfo.GatherTagList);
                            }
                            ReadDataTimer.Start();
                            StateGather.Fill = Brushes.Green;
                            UIProperty.GatherState = true;
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("1、请确认采集间隔是否为数字！\n2、请选择KepServer节点并读取！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("请填写间隔时间！");
                        return;
                    }
                    break;
            }
            if (TypeFlag == 1 || TypeFlag == 2)
            {
                XmlToBaseTimer.Start();
                CheckNormalTimer.Start();
            }
            else
            {
                MessageBox.Show("请选择采集模式！");
            }
        }

        private void ReadData(object Sender,EventArgs E)
        {
            OPCClient.ReadData();
        }

        public void ReadFileToDataBase(object Sender,EventArgs E)
        {
            MyDelegate.dReadToFile();
        }

        public void CheckIsNormal(object Sender, EventArgs E)
        {
            Task.Run(() =>
            {
                OPCClient.ReConnect(UIProperty.SelectedServerNode, UIProperty.OPCServerIP);
                SqlClient.ReConnect();
            });
        }

        private void ClickStopGather(object sender, RoutedEventArgs e)
        {
            if (UIProperty.GatherState == true)
            {
                StateGather.Fill = Brushes.Red;
                ReadDataTimer.Stop();
                XmlToBaseTimer.Stop();
                CheckNormalTimer.Stop();
                MyDelegate.dSetSubscribeType(false);
                MyDelegate.dSetReadComplete(false);
                OPCClient.DataReadEventFlag = false;
                OPCClient.DataChangedEventFlag = false;
                UIProperty.GatherState = false;
            }
            else
            {
                MessageBox.Show("程序未运行！");
            }
        }

        private void UpdateGrid(List<TableFieldInfo> TagList)
        {
            GridTagList.Clear();
            foreach (TableFieldInfo item in TagList)
            {
                GridTagList.Add(new OTSUIProperty
                {
                    TableName = item.TableName,
                    FieldName = item.FieldName,
                    TagName=item.TagName
                });
            }
            GridTag.ItemsSource = GridTagList;
        }
    }
}
