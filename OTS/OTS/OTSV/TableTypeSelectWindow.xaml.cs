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
using System.Data;
using Common.Log4net;

namespace OTS.OTSV
{
    /// <summary>
    /// TableTypeSelectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TableTypeSelectWindow : DazzleWindow
    {
        OTSUIProperty UIProperty;
        SettingFactory SettingFactory;
        SettingVirClass SettingObject;
        public TableTypeSelectWindow(OTSUIProperty UIProperty, SettingFactory SettingFactory)
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
            this.UIProperty = UIProperty;
            this.SettingFactory = SettingFactory;
            this.DataContext = UIProperty;
        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ClickTypeSelected(object sender, RoutedEventArgs e)
        {
            SettingObject = SettingFactory.GetSettingClass(UIProperty.SelectTableType);
            if (UIProperty.SavedDataBaseIP!=UIProperty.DataBaseIP||UIProperty.SelectTableType != UIProperty.SavedTableType)
            {
                MyDelegate.dReturnExample();
            }
            MyDelegate.dShowSettingPage(UIProperty.SelectTableType,SettingObject);
            this.Hide();
        }


    }
}
