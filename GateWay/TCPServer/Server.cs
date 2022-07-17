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
        public Dictionary<String, PeerConnected> ListPeerConnected = new Dictionary<string, PeerConnected>();
        Thread ListenThread;
        Thread ReceiveData;
        Thread Response;
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
                Listener = new TcpListener(IPAddress.Any, Port);
                IsActive = true;
                ListenThread = new Thread(ExecuteListenThread);
                ReceiveData = new Thread(ExecuteReceiveDataThread);
                Response = new Thread(ExecuteResponseThread);
                ListenThread.Start();
                ReceiveData.Start();
                Response.Start();
            }
            catch
            {

            }
        }
        private void ExecuteListenThread()
        {
            while (IsActive==true)
            {
                try
                {
                    Listener.Start();
                    
                    PeerConnected peerConnected = new PeerConnected(Listener.AcceptSocket());
                    if (!ListPeerConnected.ContainsKey(peerConnected.AddressEndPoint))
                    {
                        ListPeerConnected.Add(peerConnected.AddressEndPoint, peerConnected);
                    } 
                        
                }
                catch
                {

                }
            }    
        }
        private void ExecuteReceiveDataThread()
        {
            while (IsActive)
            {
                try
                {
                    if (ListPeerConnected.Count>0)
                    {
                        foreach (PeerConnected peerConnected in ListPeerConnected.Values)
                        {
                            peerConnected.ReceiveData();
                        }    
                    }    
                }
                catch
                {

                }
            }    
        }
        private void ExecuteResponseThread()
        {
            while (IsActive)
            {
                try
                {
                    if (ListPeerConnected.Count > 0)
                    {
                        foreach (PeerConnected peerConnected in ListPeerConnected.Values)
                        {
                            peerConnected.Send();
                        }
                    }
                }
                catch
                {

                } 
                 
            }    
        }

        private void GetData()
        {
            try
            {
                
            }
            catch
            {

            }
        }
    }
}
