using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace GateWay.TCPServer
{
    class Server
    {
        TcpListener Listener;
        int Port;
        public Server(String Address, int PortNumber) 
        {
            Port = PortNumber;
        }
        public void Start()
        {
            try
            {
                Listener = new TcpListener(IPAddress.Any, Port);
                Socket _Socket = Listener.AcceptSocket();
                
            }
            catch
            {

            }
        }
    }
}
