﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using IoTMessage;
namespace GateWay.TCPServer
{
    class PeerConnected
    {
        private Socket AcceptedSocket;
        public Queue<IoTMessageBase> ReceiveMessages = new Queue<IoTMessageBase>();
        public Queue<IoTMessageBase> SendMessage = new Queue<IoTMessageBase>();
        public String TargetID;
        public String AlreadySendedMessage="";
        String BeginString = "IOT=Begin";
        String EndString = "IOT=End";
        public event PeerConnectedEventHandler PeerDisconnected;
        NetworkStream _NetworkStream;
        IPAddress remoteIP;
        StringBuilder IncommingMessage= new StringBuilder();
        int port;
        int SeqNum = 0;
        int Buffer = 200;
        byte[] ReadingBuffer;
        
        Thread ReadData;
        public PeerConnected()
        {
            
        }
        public PeerConnected(Socket _socket)
        {
            AcceptedSocket = _socket;
            
            ReadingBuffer= new byte[Buffer];
            _NetworkStream = new NetworkStream(AcceptedSocket);
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
            try
            {
                int ByteRead = AcceptedSocket.Receive(ReadingBuffer, 0, Buffer, SocketFlags.None, out SocketError socketError);
                if (AcceptedSocket.Connected == true && socketError == SocketError.Success)
                {
                    if (ByteRead > 0)
                    {
                        IncommingMessage.Append(Encoding.ASCII.GetString(ReadingBuffer, 0, ByteRead));
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }    
                String ReceiveMessage = ProcessIncomminMessage();
                if (ReceiveMessage.Length > 0)
                {
                    IoTMessageBase IoTReveive = new IoTMessageBase(ReceiveMessage);
                    ReceiveMessages.Enqueue(IoTReveive);
                    return ReceiveMessage;
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
            
        }
        public String ProcessIncomminMessage()
        {
            if (IncommingMessage.Length < 10)
                return "";
            int IndexBeginString = IncommingMessage.ToString().IndexOf(BeginString);
            int IndexEndString = IncommingMessage.ToString().IndexOf(EndString);
            if (IndexBeginString<0)
            {
                IncommingMessage.Clear();
                return "";
            }    
            else if (IndexBeginString>0)
            {
                IncommingMessage.Remove(0, IndexBeginString);
                return "";
            }
            else
            {
                if (IndexEndString > 0)
                {
                    int MessageLength = IndexEndString + EndString.Length;
                    String ReturnMessage = IncommingMessage.ToString(IndexBeginString, MessageLength);
                    return ReturnMessage;
                    IncommingMessage.Remove(IndexBeginString, MessageLength);
                }
                else return "";
            } 
                
        }
       public void Send()
        {
            try
            {
                IoTMessageBase Message = new IoTMessageBase();
                if (SendMessage.Count>0)
                {
                    Message = SendMessage.Dequeue();
                    Message.SeqNum = GetSeqNum();
                    AlreadySendedMessage = Message.Build();
                    byte[] BuffContent = Encoding.ASCII.GetBytes(AlreadySendedMessage);
                    AcceptedSocket.Send(BuffContent, 0, BuffContent.Length, SocketFlags.None, out SocketError socketError);
                }
                
            }
            catch
            {
                
                throw;
            }
        }
        public void ShutDown()
        {
            AcceptedSocket.Close();
            
        }
        private int GetSeqNum()
        {
            SeqNum++;
            return SeqNum - 1;
        }
    }
}
