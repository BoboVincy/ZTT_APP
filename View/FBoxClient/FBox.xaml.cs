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
using Panuon.UI.Silver;
using BLL.IBOXClientBLL;
using Model.IBOXClient;
using System.Collections.ObjectModel;

namespace View.FBoxClient
{
    /// <summary>
    /// FBox.xaml 的交互逻辑
    /// </summary>
    public partial class FBox : WindowX
    {
        private GetIBOXServer GetServer;
        private IBOXUIproperty UIProperty;
        private Dictionary<string, string> BoxDic;
        ObservableCollection<IBOXUIproperty> GridTagList = new ObservableCollection<IBOXUIproperty>();
        public FBox()
        {
            
            InitializeComponent();
            UIProperty = new IBOXUIproperty();
            this.DataContext = UIProperty;
            init();
        }

        private void init()
        {
            MyDelegate.dShowMessage += ShowMessage;
            MyDelegate.dShowProcessMessage += ShowProcessMessage;
            MyDelegate.dGetBoxInfo += GetBoxInfo;
            MyDelegate.dGetTagInfo += UpdateGrid;
            
        }
        private void ClickLogin(object sender, RoutedEventArgs e)
        {
            GetServer = new GetIBOXServer(UIProperty.ClientID, UIProperty.ClientKey, UIProperty.UserName, UIProperty.PassWord, UIProperty.IDServer, UIProperty.APPServer, UIProperty.HDataServer);

            GetServer.IBOXLogin();
            
        }


        private void ShowMessage(string Content,string Tittle)
        {

            App.Current.Dispatcher.Invoke(new Action(() =>
            {
                MessageBoxX.Show(Content, Tittle);
            }));
            
        }

        private void GetBoxInfo(List<FBoxDeviceItem> BoxInfo)
        {
            BoxDic = new Dictionary<string, string>();
            //BoxListInfo = new List<string>();
            List<FBoxDeviceItem> ItemList = new List<FBoxDeviceItem>();
            List<FBoxDeviceItem> ItemList1 = new List<FBoxDeviceItem>();
            List<FBoxDeviceItem> ItemList2 = new List<FBoxDeviceItem>();
            BoxDic.Clear();
            //BoxListInfo.Clear();
            foreach (FBoxDeviceItem Item in BoxInfo)
            {
                //BoxListInfo.Add(Item.BoxName);
                switch (Item.BoxState) {
                    case "Connected":
                        ItemList1.Add(new FBoxDeviceItem
                        {
                            BoxName = Item.BoxName
                        });
                        break;
                    case "Undetermined":
                        ItemList2.Add(new FBoxDeviceItem
                        {
                            BoxName = Item.BoxName
                        });
                        break;
                }
                    

                BoxDic.Add(Item.BoxName, Item.BoxNO);
            }
            ItemList.Add(new FBoxDeviceItem
            {
                GroupName = "在线",
                Children = ItemList1
            });
            ItemList.Add(new FBoxDeviceItem
            {
                GroupName = "离线",
                Children = ItemList2
            });
            TreeView.Dispatcher.Invoke(new Action(() =>
            {
                TreeView.ItemsSource = ItemList;
                TreeView.Items.Refresh();
            }));
            //BoxList.Dispatcher.Invoke(new Action(() =>
            //{
            //    BoxList.ItemsSource = BoxListInfo;
            //    BoxList.Items.Refresh();
            //}));
        }

        private void ShowProcessMessage(string Content)
        {
            MessageList.Dispatcher.Invoke(new Action(() =>
            {
                MessageList.Items.Add(Content);
                MessageList.Items.Refresh();
            }));
        }

        private void ClickOpen(object sender, RoutedEventArgs e)
        {
            GetServer.GetBoxInfo();
        }


        private void UpdateGrid(List<FBoxTagInfo> TagList)
        {
            
            GridTag.Dispatcher.Invoke(new Action(() =>
            {
                GridTagList.Clear();
                foreach (FBoxTagInfo item in TagList)
                {
                    GridTagList.Add(new IBOXUIproperty
                    {
                        TagName = item.TagName,
                        GroupName = item.GroupName,
                        TagAddress = item.TagMainAddress,
                        TagValue = item.TagValue
                    });
                }
                GridTag.ItemsSource = GridTagList;
                GridTag.Items.Refresh();
            }));
        }



        private void ClickGetTag(object sender, RoutedEventArgs e)
        {
            string BoxName = (TreeView.SelectedItem as FBoxDeviceItem).BoxName;
            if (BoxName != null)
            {
                GetServer.GetTagInfo(BoxDic[BoxName]);
            }
        }

    }
}
