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
using System.Threading;
using IoTMessage;
using Client.View;

using Client.TCPClient;
namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String AccountID;
        IoTClient _IoTClient = new IoTClient();
        String IPAddress = "127.0.0.1";
        int Port = 9000;
        int SeqNum = 0;
        Thread ReadMessage;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
            Closed += MainWindowClosed;

        }

        private void btnClickSend(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbSendMesage.Text))
            {

                MessageBox.Show("Chưa nhập Message gửi");
            }
            else
            {
                String Message = tbSendMesage.Text;
                ShowSendedMessage(Message);
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
                ReadMessage = new Thread(() =>
                {
                    while (true)
                    {
                        String RecceiveMessage = _IoTClient.ClientReceiveData();
                        if (RecceiveMessage == "")
                            Thread.Sleep(1000);
                        else
                        {
                            IoTMessageBase Message = new IoTMessageBase(RecceiveMessage);
                            ShowReceiveMessage(Message);
                        }
                    }
                });
                ReadMessage.Start();
            }

        }
        private void MainWindowLoaded(object sender, EventArgs e)
        {

        }
        private void MainWindowClosed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        private void LoginClick(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
            if (frm.Username != "" && frm.Password != "")
            {
                IoTMessageBase Message = new IoTMessageBase();
                Message.Account = frm.Username;
                AccountID = frm.Username;
                Message.SeqNum = GetSeqNum();
                Message.Password = frm.Password;
                Message.MessageType = MessageType.Login;
                tbSendMesage.Text = Message.Build();
            }
        }
        private void LogOutClick(object sender, EventArgs e)
        {

        }
        private void ClickMenuItemOther(object sender, EventArgs e)
        {
            try
            {
                Controller _frm = new Controller();
                _frm.ShowDialog();
                try
                {
                    IoTMessageBase Message = _frm.Message;
                    Message.Account = AccountID;
                    Message.SeqNum = GetSeqNum();
                    Message.SenderID = AccountID;
                    tbSendMesage.Text = Message.Build();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            catch
            {
                throw;
            }
        }
        private delegate void ShowNewMessageReceive(IoTMessageBase Message);
        private void ShowReceiveMessage(IoTMessageBase Message)
        {
            try
            {
                if (!lsvReceiveMessage.Dispatcher.CheckAccess())
                {
                    lsvReceiveMessage.Dispatcher.Invoke(new ShowNewMessageReceive(ShowReceiveMessage), Message);
                }
                else
                {
                    
                    DisplayData DisplayMessageItem = new DisplayData
                    {
                        Time = DateTime.Now.ToString("HH:MM:ss"),
                        SeqNum = Message.SeqNum.ToString(),
                        MessageType = Message.MessageType.ToString(),
                        MessageRaw = Message.Build(),
                        
                    };
                    lsvReceiveMessage.Items.Add(DisplayMessageItem);
                    
                }
            }
            catch
            {
                throw;
            }
        }
        private delegate void ShowSendedMessageReceive(String Message);
        private void ShowSendedMessage(String Message)
        {
            try
            {
                if (!lsvSendMessage.Dispatcher.CheckAccess())
                {
                    lsvSendMessage.Dispatcher.Invoke(new ShowSendedMessageReceive(ShowSendedMessage), Message);
                }
                else
                {
                    try
                    {
                        IoTMessageBase IoT = new IoTMessageBase(Message);
                        DisplayData DisplayMessageItem = new DisplayData
                        {
                            Time = DateTime.Now.ToString("HH:MM:ss"),
                            SeqNum = IoT.SeqNum.ToString(),
                            MessageType = IoT.MessageType.ToString(),
                            MessageRaw = Message
                        };
                        lsvSendMessage.Items.Add(DisplayMessageItem);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private int GetSeqNum()
        {
            SeqNum++;
            return SeqNum-1;
            
        }    
    }
}
