using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMessage
{
    class IoTMessageBase
    {
        protected int _MessageType { get; set; }
        protected long _SqeNum { get; set; }
        protected String _Account { get; set; }
        protected String _HomeID { get; set; }
        protected int _Status { get; set; }
        protected int OnOff { get; set; }
        protected int _DeviceType { get; set; }
        protected int _Degree { get; set; }
        protected int _FanLevel { get; set;}
        
        /// <summary>
        /// Xác định loại Message
        /// 1= Login 2=Logout 3= Control  4=Response
        /// </summary>
        public int MessageType
        {
            get
            {
                return _MessageType;
            }
            set
            {
                this._MessageType = value;
            }
        }
        /// <summary>
        /// Xác định thứ tự Message => xử lý trùng lặp, mất gói tin
        /// </summary>
        public long SeqNum
        {
            get
            {
                return this._SqeNum;
            }
            set
            {
                this._SqeNum = value;
            }
        }
        /// <summary>
        /// Xác định định danh người dùng
        /// </summary>
        public String Account 
        {
            get
            {
                return _Account;
            } set 
            {
                _Account = value;
            } 
        }
        /// <summary>
        /// Xác định nhà thông minh được ra lệnh
        /// </summary>
        public String HomeID 
        {
            get
            {
                return _HomeID;
            }
            set 
            {
                _HomeID = value;
            } 
        }

        public int DeviceType
        {
            get
            {
                return _DeviceType; 
            }
            set
            {
                _DeviceType = value;
            }
        }
    }
}
