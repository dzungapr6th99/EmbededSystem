using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace GateWay.TCPServer
{
    class Server
    {
        TcpListener Listener;
        int Port;
        String Address;
        public bool IsActive = false;
        public event PeerConnectedEventHandler NewConnected;
        public event PeerConnectedMsgEventHandler OnReceiverMessage;
        public event PeerConnectedEventHandler PeerDisconnect;
        private object LockRecovery= new object();
        Thread ListenThread;

        public Server(String address, int PortNumber)
        {
            Address = address;
            Port = PortNumber;
        }
        public Server()
        { }
        public void Start()
        {
          
            try
            {
                if (IsActive==false)
                {
                    Listener = new TcpListener(IPAddress.Parse(Address), Port);
                    IsActive = true;
                    ListenThread = new Thread(ExecuteListenThread);
                    ListenThread.IsBackground = true;
                    ListenThread.Start();
                }
                
               
            }
            catch
            {
                IsActive = false;
            }
        }
        private void ExecuteListenThread()
        {
            while (IsActive == true)
            {
                try
                {
                    lock (LockRecovery) {  }
                    Listener.Start();

                    Socket _Socket = Listener.AcceptSocket();

                    PeerConnected peerConnected = new PeerConnected(_Socket);

                    Listener.Stop();

                    peerConnected.PeerDisconnected += PeerConnected_DisConnect;
                    if (NewConnected != null) 
                        NewConnected(this, new PeerConnectedEventArgs(peerConnected));
                    Thread ReceiveData = new Thread(ReadDataFromSocket);
                    ReceiveData.IsBackground = true;
                    ReceiveData.Start(peerConnected);
                }
                catch (Exception ex)
                {
                    
                    Thread.Sleep(1000);
                }
            }
        }
        private void PeerConnected_DisConnect(object sender, PeerConnectedEventArgs e)
        {
            if (PeerDisconnect != null)
            {
                PeerDisconnect(this, new PeerConnectedEventArgs(e.peerConnected));
            } 
                
        }    
        private void ReadDataFromSocket(object m_PeerConnected)
        {
            PeerConnected peerConnected = (PeerConnected)m_PeerConnected;
            try
            {
                while (peerConnected.IsActive)
                {
                    string MessageRecive = peerConnected.ReadMessage();
                    //khởi tạo event nhận được message
                    if ((OnReceiverMessage!=null) && (MessageRecive!="")) 
                    {
                        OnReceiverMessage(this, new PeerConnectedMsgEventArgs(peerConnected, MessageRecive));
                    }    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
             
        }

    }
}
