using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GateWay.TCPServer
{
    class PeerConnected
    {
        private Socket AcceptedSocket;
        public Queue<String> ReceiveMessages = new Queue<string>();
        public Queue<String> SendMessage = new Queue<string>();
        NetworkStream _NetworkStream;
        StreamReader Reader;
        StreamWriter Writer;
        Thread ReadData;
        Thread Response;
        public PeerConnected(Socket _socket)
        {
            AcceptedSocket = _socket;
            _NetworkStream = new NetworkStream(AcceptedSocket);
            Reader = new StreamReader(_NetworkStream);
            Writer = new StreamWriter(_NetworkStream);
            
        }
        ~PeerConnected()
        {
            ReadData.Abort();
            Response.Abort();
            ReceiveMessages.Clear();
            SendMessage.Clear();

        }
        public void Start()
        {
            ReadData = new Thread(() => 
            {
                while (true)
                {
                    String ReadData = Reader.ReadLine();
                    if (!String.IsNullOrEmpty(ReadData))
                        ReceiveMessages.Enqueue(ReadData);
                }
            });
            Response = new Thread(()=>
            {
                while (true)
                {
                    if (SendMessage.Count != 0)
                    {
                        String Message = SendMessage.Dequeue();
                        Writer.WriteLine(Message);

                    }
                    else
                        Thread.Sleep(1000);
                }    
            });
            ReadData.Start();
            Response.Start();

        }
       
    }
}
