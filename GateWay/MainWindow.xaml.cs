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
using GateWay.TCPServer;
//using IoTMessage;
namespace GateWay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Server c_TCPServer;
        Dictionary<String, PeerConnected> PeerConnectedList = new Dictionary<string, PeerConnected>();
        List<DisplayPeerConnected> ListClientAddress = new List<DisplayPeerConnected>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += LoadMainWindow;
            Closed += CloseMainWindow;
           
        }

        public void LoadMainWindow(object sender, EventArgs e)
        {
            String Address = "127.0.0.1";
            int Port = 9000;
            c_TCPServer = new Server(Address, Port);
            c_TCPServer.NewConnected += TcpServerNewConnected;
            c_TCPServer.OnReceiverMessage += TcpServerReceiveMessage;
            c_TCPServer.PeerDisconnect += TcpServerDisconnect;
            
        }

        public void CloseMainWindow(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void Click_btnStartServer(object sender, RoutedEventArgs e)
        {
            c_TCPServer.Start();
            btnStartServer.Content = "Server Started";
            btnStartServer.Foreground = Brushes.Green;
        }


        private void TcpServerNewConnected(object sender,PeerConnectedEventArgs e)
        {
            String _key = e.peerConnected.AddressEndPoint;
            PeerConnectedList.Add(_key, e.peerConnected);
            ReloadPeerConnectedList();
        }

        private void TcpServerReceiveMessage(object sender, PeerConnectedMsgEventArgs e)
        {
            String _key = e.PeerConnected.AddressEndPoint+":"+e.PeerConnected.Port.ToString();
            ShowNewMessage(_key, e.Msg);
        }
        private delegate void ShowNewMessageReceive(string peerkey, string Message);
        private void ShowNewMessage(String peerkey, String Message)
        {
            try
            {
                if (!lsvReceiveMessage.Dispatcher.CheckAccess())
                {
                    lsvReceiveMessage.Dispatcher.Invoke(new ShowNewMessageReceive(ShowNewMessage), peerkey, Message);
                }
                else
                {
                    DisplayData DisplayMessageItem = new DisplayData
                    {
                        Client = peerkey,
                        MessageRaw = Message,
                        Time = DateTime.Now.ToString("HH:MM:ss"),
                        SeqNum = "0"
                    };
                    lsvReceiveMessage.Items.Add(DisplayMessageItem);
                }
            }
            catch
            {
                throw;
            }
            
        }
        private void TcpServerDisconnect(object sender, PeerConnectedEventArgs e)
        {

        }
        private delegate void NewConnectDelegate();
        private void ReloadPeerConnectedList()
        {
            try
            {
                if (!lsvPeerConnected.Dispatcher.CheckAccess())
                {
                    lsvPeerConnected.Dispatcher.Invoke(new NewConnectDelegate(ReloadPeerConnectedList));

                }   
                else
                    try
                    {
                        
                        foreach (KeyValuePair<String, PeerConnected> entry in PeerConnectedList)
                        {
                            DisplayPeerConnected PeerConnectedItem = new DisplayPeerConnected()
                            {
                                Time = DateTime.Now.ToString(),
                                Address = entry.Key
                            };
                            lsvPeerConnected.Items.Add(PeerConnectedItem);
                        }
                    }
                    catch
                    {
                        throw;
                    }
            }
            catch
            {
                throw;
            }
            
        }
    }
}
