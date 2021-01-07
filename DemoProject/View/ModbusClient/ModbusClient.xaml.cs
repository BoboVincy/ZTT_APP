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
using Common.Serial;
using Model.ModbusModel;
using BLL.ModbusClient;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace View.ModbusClient
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class ModbusClient : Window
    {
        private ModbusUIProperty UIProperty;
        ModbusBase Client;
        private PortSetting PortSettingWindow;
        private ModbusSetting ModbusSettingWindow;
        List<TagListInfo> TagList;
        ObservableCollection<ModbusUIProperty> GridTagList = new ObservableCollection<ModbusUIProperty>();
        System.Windows.Threading.DispatcherTimer ReadDataTimer = new System.Windows.Threading.DispatcherTimer();
        public ModbusClient()
        {
            InitializeComponent();
            Init();
            this.DataContext = UIProperty;
            PortSettingWindow = new PortSetting(UIProperty);
            ModbusSettingWindow = new ModbusSetting(UIProperty);
            ReadDataTimer.Tick += new EventHandler(GetTagValue);
            ReadDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            this.Closing += CloseWindow;
            MyDelegate.dSetTimer += SetTimer;
        }

        private void Init()
        {
            UIProperty = new ModbusUIProperty();
        }

        private void ClickPortSetting(object Sender, RoutedEventArgs e)
        {
            PortSettingWindow.Show();
        }

        private void SetTimer()
        {
            if (!ReadDataTimer.IsEnabled)
            {
                ReadDataTimer.Start();
            }
        }

        private void GetTagValue(object Sender,EventArgs E )
        {
            Client = MyDelegate.dSetModbusBLL();
            TagList = new List<TagListInfo>();
            Client.Send(Convert.ToInt32(UIProperty.DeviceNum), UIProperty.RegisterType, Convert.ToUInt16(UIProperty.StartBit), Convert.ToUInt16(UIProperty.Length));
            string RegisterType = GetAdressType(UIProperty.RegisterType);
            int StartBit = Convert.ToInt32(UIProperty.StartBit);
            int Length = Convert.ToInt32(UIProperty.Length);
            if (Client.TagValueList!=null)
            {
                if (Client.TagValueList.Count == Length)
                {
                    for (int i = StartBit; i <= Length; i++)
                    {
                        TagList.Add(new TagListInfo
                        {
                            RegisterName = RegisterType + string.Format("{0:D4}", i),
                            RegisterValue = Client.TagValueList[i - StartBit].ToString()
                        });
                    }
                    Client.TagValueList.Clear();
                }
            }
            UpdateGrid(TagList);
            GridTag.Items.Refresh(); 
        }

        private string GetAdressType(string RegisterType)
        {
            string Adr = "0";
            switch (RegisterType)
            {
                case "1x":
                    Adr = "0";
                    break;
                case "2x":
                    Adr = "1";
                    break;
                case "3x":
                    Adr = "4";
                    break;
                case "4x":
                    Adr = "3";
                    break;
            }
            return Adr;
        }
        private void UpdateGrid(List<TagListInfo> TagList)
        {
            GridTagList.Clear();
            foreach (TagListInfo item in TagList)
            {
                GridTagList.Add(new ModbusUIProperty
                {
                    Register = item.RegisterName,
                    RegisterValue = item.RegisterValue,
                });
            }
            GridTag.ItemsSource = GridTagList;
        }

        private void CloseWindow(object Sender, EventArgs E)
        {
            System.Diagnostics.Process RunProcess = System.Diagnostics.Process.GetProcessById(Process.GetCurrentProcess().Id);
            RunProcess.Kill();
        }

        private void ClickModbusSetting(object sender, RoutedEventArgs e)
        {
            ModbusSettingWindow.Show();
        }
    }
}
