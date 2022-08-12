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
            /*DisplayPeerConnected peerItem = new DisplayPeerConnected()
            {
                Address = "192.168.1.1",
                Time = DateTime.Now.ToString()
            };
            lsvPeerConnected.Items.Add(peerItem);*/
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
            DisplayData DisplayMessageItem = new DisplayData
            {
                Client = e.PeerConnected.AddressEndPoint + ":" + e.PeerConnected.Port,
                MessageRaw = e.PeerConnected.ReadMessage(),
                Time = DateTime.Now.ToString("HH:MM:ss")
            };
            lsvReceiveMessage.Items.Add(DisplayMessageItem);
        }
        private void TcpServerDisconnect(object sender, PeerConnectedEventArgs e)
        {

        }
       private void ReloadPeerConnectedList()
        {
            foreach (KeyValuePair<String, PeerConnected> entry in PeerConnectedList )
            {
                DisplayPeerConnected PeerConnectedItem = new DisplayPeerConnected()
                {
                    Time = DateTime.Now.ToString(),
                    Address = entry.Key
                };
                lsvPeerConnected.Items.Add(PeerConnectedItem);
                int j = PeerConnectedList.Count();
            }
            
        }
    }
}
