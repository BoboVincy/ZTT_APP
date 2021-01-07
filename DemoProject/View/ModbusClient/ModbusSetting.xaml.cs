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
using Model.ModbusModel;
using BLL.ModbusClient;

namespace View.ModbusClient
{
    /// <summary>
    /// ModbusSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ModbusSetting : Window
    {
        ModbusUIProperty UIProperty;
        ModbusBase Client;
        public ModbusSetting(ModbusUIProperty UIProperty)
        {
            InitializeComponent();
            this.UIProperty = UIProperty;
            this.DataContext = UIProperty;
            Closing += CloseWindow;
            List<string> TypeList = new List<string>() { "1x", "2x", "3x", "4x" };
            this.ComboRegister.ItemsSource = TypeList;
            this.ComboRegister.SelectedIndex = 0;
        }

        private void ClickModbusSetting(object sender, RoutedEventArgs e)
        {
            Client = MyDelegate.dSetModbusBLL();
            this.Hide();
            MyDelegate.dSetTimer();
            MyDelegate.dSetTCPRecieveTimer();
        }


        private void CloseWindow(object Object, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
