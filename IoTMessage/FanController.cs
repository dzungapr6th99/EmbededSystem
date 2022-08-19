using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMessage
{
    public class FanController:IoTMessageBase
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
        public FanController()
        {
            
        }

    }
}
