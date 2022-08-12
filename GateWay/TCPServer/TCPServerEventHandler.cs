using System;
using System.Collections.Generic;
using System.Text;
using System;
namespace GateWay.TCPServer
{
    delegate void PeerConnectedMsgEventHandler(object sender, PeerConnectedMsgEventArgs e);

    class PeerConnectedMsgEventArgs : EventArgs
    {
        private PeerConnected _peerConnected;
        public String _msg;

        public PeerConnectedMsgEventArgs(PeerConnected peerConnected, String S)
        {
            _peerConnected = peerConnected;
            _msg = S;
        }

        public PeerConnected PeerConnected
        {
            get
            {
                return _peerConnected;
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

    delegate void PeerConnectedEventHandler(object sender, PeerConnectedEventArgs e);
    class PeerConnectedEventArgs : EventArgs
    {
        private PeerConnected _peerConnected;

        public PeerConnectedEventArgs(PeerConnected peerConnected)
        {
            _peerConnected = peerConnected;
        }

        public PeerConnected peerConnected
        {
            get
            {
                return _peerConnected;
            }
        }
    }
    

}
