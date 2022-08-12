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
        public event PeerConnectedEventHandler PeerDisconnected;
        NetworkStream _NetworkStream;
        IPAddress remoteIP;
        int port;
        StreamReader Reader;
        StreamWriter Writer;
        Thread ReadData;
        public PeerConnected()
        {

        }
        public PeerConnected(Socket _socket)
        {
            AcceptedSocket = _socket;
            _NetworkStream = new NetworkStream(AcceptedSocket);
            Reader = new StreamReader(_NetworkStream);
            Writer = new StreamWriter(_NetworkStream);
            IPEndPoint ipend = (IPEndPoint)AcceptedSocket.RemoteEndPoint;
            remoteIP = ipend.Address;
            port = ipend.Port;
        }
        ~PeerConnected()
        {

        }
        public IPAddress RemoteIP
        {
            get 
            {
                return remoteIP;
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
        }
        public String AddressEndPoint
        {
            get
            {
                return RemoteIP.ToString() + ":" + Port.ToString();
            }
        }
        public bool IsActive
        {
            get
            {
                return AcceptedSocket.Connected;
            }
        }
        public String ReadMessage()
        {
          
           String ReadData = Reader.ReadLine();
           if (!String.IsNullOrEmpty(ReadData))
               ReceiveMessages.Enqueue(ReadData);        
          return ReadData;
        }
       public void Send()
        {
            while (SendMessage.Count>0)
            {
                String Message = SendMessage.Dequeue();
                Writer.Write(Message);
            }
        }
    }
}
