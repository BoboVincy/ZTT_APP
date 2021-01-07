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
using Common.Log4net;

namespace View.ModbusClient
{
    /// <summary>
    /// PortSetting.xaml 的交互逻辑
    /// </summary>
    public partial class PortSetting : Window
    {
        private List<string> PortList = new List<string>() { "COM1" ,"COM2","COM3","COM4","COM5","COM6","COM7"};
        private List<string> BaudList = new List<string>() { "4800", "9600", "14400", "19200" };
        private List<string> DataList = new List<string>() { "7", "8" };
        private List<string> ParityList = new List<string>() { "奇", "偶", "无" };
        private List<string> StopBitsList = new List<string>() { "1", "1.5", "2" };
        private ModbusUIProperty UIProperty;
        private ModbusFactory Factory = new ModbusFactory();
        public ModbusBase Client;

        public PortSetting(ModbusUIProperty UIProperty)
        {
            InitializeComponent();
            this.UIProperty = UIProperty;
            this.DataContext = UIProperty;
            ComboPort.ItemsSource = PortList;
            ComboPort.SelectedItem = PortList[0];
            ComboBaud.ItemsSource = BaudList;
            ComboBaud.SelectedItem = BaudList[0];
            ComboData.ItemsSource = DataList;
            ComboData.SelectedItem = DataList[0];
            ComboParity.ItemsSource = ParityList;
            ComboParity.SelectedItem = ParityList[0];
            ComboStop.ItemsSource = StopBitsList;
            ComboStop.SelectedItem = StopBitsList[0];
            Closing += CloseWindow;
            MyDelegate.dSetModbusBLL += GetModbusBLL;
            InitialRouteEvents();
        }

        private void ClickConnect(object sender, RoutedEventArgs e)
        {
            bool Result = Client.Connect(UIProperty.PortName, UIProperty.BaudRate, UIProperty.DataBit, UIProperty.Parity, UIProperty.StopBits);
            this.Hide();
            if (Result)
            {
                MessageBox.Show("Connect Success!");
            }
            else
            {
                MessageBox.Show("Connect Failed");
            }
        }

        private void CloseWindow(object Object, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        /// <summary>
        /// 查询类型按键添加路由事件
        /// </summary>
        private void InitialRouteEvents()
        {
            try
            {
                this.PanelModbusType.AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioChecked));
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error("预警类型路由事件失败：" + ex.ToString());
            }
        }

        private void RadioChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                UIElement s = (UIElement)e.OriginalSource;
                RadioButton button = new RadioButton();
                button = s as RadioButton;
                switch (button.Name)
                {
                    case "RadioButtonRTU":
                        this.PanelPortSetting.Visibility = Visibility.Visible;
                        this.PanelModbusType.Visibility = Visibility.Hidden;
                        this.PanelModbusTCP.Visibility = Visibility.Hidden;
                        UIProperty.Modbustype = "RTU";
                        break;
                    case "RadioButtonTCP":
                        this.PanelPortSetting.Visibility = Visibility.Hidden;
                        this.PanelModbusType.Visibility = Visibility.Hidden;
                        this.PanelModbusTCP.Visibility = Visibility.Visible;
                        UIProperty.Modbustype = "TCP";
                        break;
                }
                Client = Factory.ModbusBLL(UIProperty.Modbustype);
            }
            catch (Exception ex)
            {
                LogHelp.Log.Error(ex.ToString());
            }
        }

        private void ClickConnectTCP(object sender, RoutedEventArgs e)
        {
            bool Result = Client.Connect(UIProperty.TCPIP, Convert.ToInt32(UIProperty.TCPPort));
            this.Hide();
            if (Result)
            {
                MessageBox.Show("Connect Success!");
            }
            else
            {
                MessageBox.Show("Connect Failed");
            }
        }

        private ModbusBase GetModbusBLL()
        {
            return Client;
        }
    }
}
