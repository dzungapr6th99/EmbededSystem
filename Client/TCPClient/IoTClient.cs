using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using IoTMessage;
namespace Client.TCPClient
{
    class IoTClient
    {
        TcpClient Client = new TcpClient();
        NetworkStream stream;
        static Encoding encoding = new ASCIIEncoding();
        Queue<String> SendMessage = new Queue<string>();
        StringBuilder IncommingMessage = new StringBuilder();
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

        public String ClientReceiveData()
        {
            String Result = "";
            try
            {
                
                // 3. receive
                byte[] data = new byte[200];
                stream.Read(data, 0, data.Length);
                IncommingMessage.Append( Encoding.ASCII.GetString(data));
                if (IncommingMessage.Length < 10)
                    return "";
                else
                {
                    int IndexBegin = IncommingMessage.ToString().IndexOf(IoTMessageBase.IOTBegin);
                    int IndexEnd = IncommingMessage.ToString().IndexOf(IoTMessageBase.IOTEnd);
                    if (IndexBegin < 0)
                    {
                        IncommingMessage.Clear();
                        return "";
                    }
                    else if (IndexBegin > 0)
                    {
                        IncommingMessage.Remove(0, IndexBegin);
                        return "";
                    }
                    else
                    {
                        if (IndexEnd > 0)
                        {
                            int MessageLength = IndexEnd + IoTMessageBase.IOTEnd.Length;
                            String ReturnMessage = IncommingMessage.ToString(IndexBegin, MessageLength);
                            return ReturnMessage;
                            IncommingMessage.Remove(IndexBegin, MessageLength);
                        }
                        else return "";
                    }
                }    
            }
            catch
            {
                return Result;
            }
        }    

        public void PushMessage(String Message)
        {
            
            SendMessage.Enqueue(Message);
            //byte[] data = encoding.GetBytes(Message);
            //stream.Write(data, 0, data.Length);
            var SendBuffer = Encoding.ASCII.GetBytes(Message);
            stream.Write(SendBuffer, 0, SendBuffer.Length);
        }
         
    }
}
