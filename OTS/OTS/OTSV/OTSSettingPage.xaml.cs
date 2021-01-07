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
using Model.OTSM;
using BLL.OTSC;
using Common.Log4net;

namespace OTS.OTSV
{
    /// <summary>
    /// OTSSettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class OTSSettingPage : UserControl
    {
        SettingVirClass SettingObject;

        public OTSSettingPage(SettingVirClass SettingObject)
        {
            InitializeComponent();
            this.SettingObject = SettingObject;
        }


        private void ClickTableRead(object sender, RoutedEventArgs e)
        {
            SettingObject.ReadTable();
            this.TableFieldView.Items.Refresh();
        }

        private void ClickServerRead(object sender, RoutedEventArgs e)
        {
            SettingObject.ReadServer();
            this.KepServerTagView.Items.Refresh();
        }

        private void ClickImport(object sender, RoutedEventArgs e)
        {
            MyDelegate.dShowPairWindow();
        }

        private void ClickAddItem(object sender, RoutedEventArgs e)
        {
            SettingObject.AddItem();
            this.ShowViewList.Items.Refresh();

        }

        private void ClickTableChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.TableChanged();
            this.ShowViewList.Items.Refresh();
        }

        private void ClickAutoPair(object sender, RoutedEventArgs e)
        {
            SettingObject.AutoPair();
            this.ShowViewList.Items.Refresh();
        }

        private void ClickChannelChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.ChannelChanged();
            this.DeviceList.Items.Refresh();
        }

        private void ClickDeviceChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.DeviceChanged();
            this.KepServerTagView.Items.Refresh();
        }

        private void ClickServerChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.ServerChanged();
        }

        private void ClickSelectedResult(object sender, RoutedEventArgs e)
        {
            SettingObject.SelectedResult();
        }

        private void ClickDeleteItem(object sender, RoutedEventArgs e)
        {
            SettingObject.DeleteItem();
            this.ShowViewList.Items.Refresh();
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {

            SettingObject.Save();
        }


    }
}
