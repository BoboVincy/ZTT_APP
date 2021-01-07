using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Panuon.UI.Silver;
using Common.MQTT.BLL;
using Common.MQTT.Model;
using Model.MQTTModel;


namespace View.MQTT
{
    /// <summary>
    /// MQTTClient.xaml 的交互逻辑
    /// </summary>
    public partial class MQTTClient : WindowX
    {
        MQTTCommon Client;
        LoginModel LoginParam;
        MQTTUIProperty UIProperty;
        System.Windows.Threading.DispatcherTimer ReadDataTimer = new System.Windows.Threading.DispatcherTimer();
        public MQTTClient()
        {
            InitializeComponent();
            ReadDataTimer.Interval = new TimeSpan(0, 0, 0, 5);
            ReadDataTimer.Tick += new EventHandler(ReadList);
            Init();
        }

        public void Init()
        {
            UIProperty = new MQTTUIProperty();
            this.DataContext = UIProperty;
        }


        private void ClickConnect(object sender, RoutedEventArgs e)
        {
            LoginParam = new LoginModel
            {
                Host = UIProperty.Host,
                Port = Convert.ToInt32(UIProperty.Port),
                UserName = UIProperty.UserName,
                PassWord = UIProperty.PassWord
            };
            Client = new MQTTCommon(LoginParam);
            Task.Run(async () =>
            {
                await Connect();
            });
            LodingIcon.Dispatcher.Invoke(new Action(() =>
            {
                LodingIcon.Visibility = Visibility.Visible;
            }));

        }

        public async Task Connect()
        {
            try
            {
                bool Result = await Client.MQTTConnect();
                if (Result == true)
                {
                    ConnectState.Dispatcher.Invoke(new Action(() =>
                    {
                        ConnectState.Background = Brushes.Green;
                        
                    }));
                    LodingIcon.Dispatcher.Invoke(new Action(() =>
                    {
                        LodingIcon.Visibility = Visibility.Hidden;
                    }));
                    PanelTopic.Dispatcher.Invoke(new Action(() =>
                    {
                        PanelTopic.Visibility = Visibility.Visible;
                    }));
                }
                else
                {
                    ConnectState.Dispatcher.Invoke(new Action(() =>
                    {
                        ConnectState.Background = Brushes.Red;
                    }));
                }
            }
            catch (Exception EX)
            {
            }
        }

        private void ClickSubscribe(object sender, RoutedEventArgs e)
        {

            Task.Run(async () =>
            {
                await Subscribe();
            });
        }

        public async Task Subscribe()
        {
            try
            {
                bool Result = await Client.MQTTSubscribe(UIProperty.Topic);
                if (Result == true)
                {
                    ListBoxStatue.Dispatcher.Invoke(new Action(() =>
                    {
                        ListBoxStatue.Items.Add("订阅" + UIProperty.Topic + "成功! " + DateTime.Now);
                        ReadDataTimer.Start();
                    }));
                }
                else
                {
                    ListBoxStatue.Dispatcher.Invoke(new Action(() =>
                    {
                        ListBoxStatue.Items.Add("订阅" + UIProperty.Topic + "失败! " + DateTime.Now);
                    }));
                }
            }
            catch (Exception EX)
            {

            }
        }

        private void ReadList(object sender,EventArgs e )
        {
            if (Client != null)
            {

                ListBoxMessage.Dispatcher.Invoke(new Action(() =>
                {
                    ListBoxMessage.ItemsSource = Client.MessageList;
                }));
                ListBoxMessage.Dispatcher.Invoke(new Action(() =>
                {
                    ListBoxMessage.Items.Refresh();
                }));
            }
        }


    }
}
