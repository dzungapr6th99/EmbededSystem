﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOTMessage;
namespace IOTMessage
{
    class IoTFanControllerMessage : IoTMessageBase
    {
        /// <summary>
        /// Tham số điều chỉnh mức gió của quạt
        /// </summary>
        public int FanLevel
        {
            get 
            {
                return _FanLevel;
            }
            set
            {
                _FanLevel = value;
            }
        }

        
    }
}
