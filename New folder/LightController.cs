using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTMessage
{
    class LightController:IoTMessageBase
    {
        public int OnOff
        {
            get
            {
                return OnOff;
            }
            set
            {
                OnOff = value;
            }
        }

        
    }
}
