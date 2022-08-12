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
using Client.TCPClient;
namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IoTClient _IoTClient = new IoTClient();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
            Closed += MainWindowClosed;
            
        }

        private void btnClickSend(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbSendMesage.Text))
                MessageBox.Show("Chưa nhập Message gửi");
            else
            {
                String Message = tbSendMesage.Text;
                _IoTClient.PushMessage(Message);
                tbSendMesage.Text = "";
            }    
                

        }
        private void ConnectToServer(object sender, RoutedEventArgs e)
        {
            _IoTClient.Connect("127.0.0.1", 9000); //Tên miền IP, Port 
            if (_IoTClient.IsConnected)
            {
                lbIpConnect.Content = _IoTClient.IPConnect;
                lbIpConnect.Background = Brushes.Green;
            }    
        }
        private void MainWindowLoaded(object sender, EventArgs e)
        {

        }
        private void MainWindowClosed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
