using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Panuon.UI.Silver;
using Model.OPCClientModel;
using BLL.OPCClientBLL;
using System.Collections.ObjectModel;

namespace View.OPCClient
{
    /// <summary>
    /// ClientMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClientMainWindow : WindowX
    {
        ClientUIProperty UIProperty;
        List<string> NodeList;
        List<TagListInfo> TagList = new List<TagListInfo>();
        ObservableCollection<ClientUIProperty> GridTagList = new ObservableCollection<ClientUIProperty>();
        //定时器
        System.Windows.Threading.DispatcherTimer ReadDataTimer = new System.Windows.Threading.DispatcherTimer();
        public ClientMainWindow()
        {

            InitializeComponent();
            UIProperty = new ClientUIProperty();
            this.Loaded += GetNodeClicked;
            ReadDataTimer.Tick += new EventHandler(ReadData);
            ReadDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            Init();
        }

        public void Init()
        {
            Root.DataContext = UIProperty;
            NodeList = new List<string>();
            MyDelegate.dShowMessageBox += ShowMessageBox;
            MyDelegate.dControlLoading += ControlNodeLoading;
        }

        private void GetNodeClicked(object sender, RoutedEventArgs e)
        {
            this.ComboNode.ItemsSource = GetServer.GetServerNode();
        }

        private void ConnectClicked(object sender, RoutedEventArgs e)
        {
            List<string> DeviceList = new List<string>();
            this.DeviceListBox.ItemsSource = DeviceList; ;
            //List<DeviceProperty> DeviceList = new List<DeviceProperty>();
            DeviceList = GetServer.GetDevice(UIProperty.SelectedServerNode, UIProperty.SelectedServerName);
            if (GetServer.GetServerConnection(UIProperty.SelectedServerNode, UIProperty.SelectedServerName))
            {
                //this.DeviceTree.ItemsSource = DeviceList;
                this.DeviceListBox.ItemsSource = DeviceList;
                this.DeviceListBox.Items.Refresh();
            }
            else
            {
                MessageBoxX.Show("连接错误！", "注意");
            }
        }

        private void AddTagClicked(object sender, RoutedEventArgs e)
        {
            if (UIProperty.SelectedTag.Split('.').Length <= 3)
            {
                TagList = GetServer.GetTagValue(UIProperty.SelectedTag);
            }
            else
            {
                MyDelegate.dShowMessageBox("此点不可采集", "提示");
            }
            ReadDataTimer.Start();
            UpdateGrid(TagList);
            GridTag.Items.Refresh();
        }

        private void DeleteTagClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SelectedToGetServerName(object sender, RoutedEventArgs e)
        {
            List<string> NameList = new List<string>();
            this.NodeLoding.IsRunning = true;
            Task.Run(() =>
            {
                UIProperty.NameList = GetServer.GetServerName(UIProperty.SelectedServerNode);
                MyDelegate.dControlLoading(false);
            });  
        }

        private void ShowMessageBox(string Content, string Tittle)
        {
            MessageBoxX.Show(Content,Tittle);
        }

        private void ControlNodeLoading(bool State)
        {
            this.NodeLoding.Dispatcher.Invoke(new Action(() =>
            {
                this.NodeLoding.IsRunning = State;
            }));   
        }

        private void ReadData(object Sender, EventArgs E)
        {

            GetServer.ReadData();
            UpdateGrid(TagList);
            GridTag.Items.Refresh();
        }

        private void UpdateGrid(List<TagListInfo> TagList)
        {
            GridTagList.Clear();
            foreach (TagListInfo item in TagList)
            {
                GridTagList.Add(new ClientUIProperty
                {
                    TagName = item.Tag,
                    Value = item.Value,
                    UpdateTime = item.UpdateTime.ToLongTimeString()
                });
            }
            GridTag.ItemsSource = GridTagList;
        }


    }
}
