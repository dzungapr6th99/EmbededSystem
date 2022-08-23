using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMessage
{
    public static class MessageType
    {
        public static int Login = 1;
        public static int Logout = 2;
        public static int FanController = 3;
        public static int AirConditonController = 4;
        public static int LightController = 5;
        public static int ResendMessage = 6;
        public static int SequenceReset = 7;
    }
}
