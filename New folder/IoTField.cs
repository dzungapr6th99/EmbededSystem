using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTMessage
{
    class IoTField
    {
        private readonly int tag;
        private readonly string value;
        
        public IoTField() { }

        public IoTField(int t, string v)
        {
            tag = t;
            value = v;
        }

        public int Tag => this.tag;
        public String Value => this.value;
    }
}
