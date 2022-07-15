using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Sockets;
namespace Client.TCPClient
{
    class IoTClient
    {
        TcpClient Client = new TcpClient();
        Stream stream;
        Queue<String> SendMessage = new Queue<string>();
        Thread Communicate;
        public bool IsConnected = false;
        public string IPConnect;
        static ASCIIEncoding Encode = new ASCIIEncoding();
        public IoTClient() { }
        public void Connect(String Ip, int PortNumber)
        {
            
            while (!Client.Connected)
            {
                try
                {
                    Client.Connect(Ip, PortNumber);
                    IsConnected = true;
                    IPConnect = Client.Client.RemoteEndPoint.ToString();
                }    
                catch
                {

                }
            }    
            stream = Client.GetStream();
            Communicate = new Thread(() =>
            {
                while (true)
                {
                    if (SendMessage.Count > 0)
                    {
                        String Message = SendMessage.Dequeue();
                        byte[] data = Encode.GetBytes(Message);
                        stream.Write(data, 0, data.Length);
                    }
                    else
                        Thread.Sleep(1000);
                }    
            });
            Communicate.Start();
        }
        public void PushMessage(String Message)
        {
            SendMessage.Enqueue(Message);
        }
         
    }
}
