using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace Client.TCPClient
{
    class IoTClient
    {
        TcpClient Client = new TcpClient();
        Stream stream;
        StreamWriter Writer;
        static Encoding encoding = new ASCIIEncoding();
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
         
        }
        public void PushMessage(String Message)
        {
            SendMessage.Enqueue(Message);
            //byte[] data = encoding.GetBytes(Message);
            //stream.Write(data, 0, data.Length);
            var SendBuffer = Encoding.UTF8.GetBytes(Message);
            stream.Write(SendBuffer, 0, SendBuffer.Length);
            stream.Flush();
        }
         
    }
}
