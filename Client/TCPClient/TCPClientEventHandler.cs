using System;
using System.Collections.Generic;
using System.Text;

namespace Client.TCPClient
{
    delegate void TCPClientMsgEventHandler(object sender, PeerConnectedMsgEventArgs e);

    class PeerConnectedMsgEventArgs : EventArgs
    {
        private String ServerAddress;
        public String _msg;

        public PeerConnectedMsgEventArgs(String Address, String S)
        {
            ServerAddress=Address;
            _msg = S;
        }

        public String ServerIP
        {
            get
            {
                return ServerAddress;
            }
        }
        public String Msg
        {
            get
            {
                return _msg;
            }
        }
    }
    class TCPClientEventHandler
    {
    }
}
