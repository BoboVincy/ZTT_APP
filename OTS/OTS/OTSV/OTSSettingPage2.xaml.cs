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
    /// OTSSettingPage2.xaml 的交互逻辑
    /// </summary>
    public partial class OTSSettingPage2 : UserControl
    {
        SettingVirClass SettingObject;
        public OTSSettingPage2(SettingVirClass SettingObject)
        {
            InitializeComponent();
            this.SettingObject = SettingObject;
        }

        private void ClickTableRead(object sender, RoutedEventArgs e)
        {
            SettingObject.ReadTable();
            this.FieldList.Items.Refresh();
        }

        private void ClickServerRead(object sender, RoutedEventArgs e)
        {
            SettingObject.ReadServer();

        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            SettingObject.Save();
        }

        private void ClickTypeSave(object sender, RoutedEventArgs e)
        {
            SettingObject.TypeSave();
            this.DescriptionTableView.Items.Refresh();
        }

        private void ClickChannelChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.ChannelChanged();
        }

        private void ClickDeviceChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.DeviceChanged();
        }

        private void ClickAddItem(object sender, RoutedEventArgs e)
        {
            SettingObject.AddItem();
            this.ShowViewList.Items.Refresh();
        }

        private void ClickDeleteItem(object sender, RoutedEventArgs e)
        {
            SettingObject.DeleteItem();
            this.ShowViewList.Items.Refresh();
        }

        private void ClickTableChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.TableChanged();
        }

        private void ClickServerChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.ServerChanged();
        }

        private void ClickSelectedResult(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.SelectedResult();
        }

        private void ClickTypeDelete(object sender, RoutedEventArgs e)
        {
            SettingObject.TypeDelete();
            this.DescriptionTableView.Items.Refresh();
        }

        private void ClickTypeDescriptionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingObject.TypeDescriptionChanged();
        }
    }
}
