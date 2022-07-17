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
        public MainWindow()
        {
            InitializeComponent();
            String Address = "127.0.0.1";
            int Port = 9000;
            c_TCPServer = new Server(Address, Port);
        }

        private void Click_btnStartServer(object sender, RoutedEventArgs e)
        {
            c_TCPServer.Start();

        }
        private void lsvPeerConnectedClick(object sender, RoutedEventArgs e)
        {
            List<DisplayPeerConnected> ListPeerConnected = new List<DisplayPeerConnected>();
            foreach (PeerConnected peerConnected in c_TCPServer.ListPeerConnected.Values)
            {
                DisplayPeerConnected display = new DisplayPeerConnected
                {
                    Address = peerConnected.AddressEndPoint
                };
                ListPeerConnected.Add(display);
            
            }
            lsvPeerConnected.ItemsSource = ListPeerConnected;
            lsvPeerConnected.Items.Refresh();
        }
        class  DisplayPeerConnected
        {
            public String Address;

        }
    }
}
