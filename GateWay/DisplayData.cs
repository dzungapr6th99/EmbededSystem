using System;
using System.Collections.Generic;
using System.Text;

namespace GateWay
{
    class DisplayData
    {
        public String Time { get; set; }
        public String Client { get; set; }
        public String MessageRaw { get; set; }
    }
    class DisplayPeerConnected
    {
        public String Address { get; set; }
        public String Time { get; set; }
        public DisplayPeerConnected() { }
    }

}
