using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTMessage
{
    internal class IoTMessageBase
    {
        public static String IOTBegin = "IOT=Begin";
        public static String IOTEnd = "IOT=End";
        public String MessageRaw { get; set; }
        /// <summary>
        /// Xác định loại Message
        /// Tag: 1
        /// </summary>
        protected int _MessageType { get; set; }
        /// <summary>
        /// Xác định thứ tự message
        /// tag=2
        /// </summary>
        protected long _SeqNum { get; set; }
        /// <summary>
        /// Xác định ID người dùng
        /// Tag=3
        /// </summary>
        protected String _Account { get; set; }
        /// <summary>
        /// Xác định ID nhà cần điều khiển, 
        /// Tag=4
        /// </summary>
        protected String _HomeID { get; set; }
        /// <summary>
        /// Xác định trạng thái thiết bị
        /// Tag=5
        /// </summary>
        protected int _Status { get; set; }
        /// <summary>
        /// Lệnh điều khiển 
        /// Tag=6
        /// </summary>
        protected int OnOff { get; set; }
        /// <summary>
        /// Xác định loại thiết bị
        /// Tag=7
        /// </summary>
        protected int _DeviceType { get; set; }
        /// <summary>
        /// Xác định giá trị nhiệt độ điều hòa
        /// Tag=8
        /// </summary>
        protected int _Degree { get; set; }
        /// <summary>
        /// Xác định độ mạnh của quạt
        /// Tag=9
        /// </summary>
        protected int _FanLevel { get; set;}
        /// <summary>
        /// Xác định ID thiết bị
        /// </summary>
        protected string _DeviceID { get; set; }
        /// <summary>
        /// Kiểm tra chế độ của Resend Request
        /// tag=10
        /// </summary>
        protected int _GapFill { get; set; }
        /// <summary>
        /// Đánh dấu message bắt đầu của resend
        /// tag=11
        /// </summary>
        protected long _BeginSendNo { get; set; }
        /// <summary>
        /// Đánh dấu Message bắt đầu của 
        /// </summary>
        protected long _EndSendNo { get; set; }
        /// <summary>
        /// Xác định loại Message
        /// Tag = 1
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
        /// Tag=2
        /// </summary>
        public long SeqNum
        {
            get
            {
                return this._SeqNum;
            }
            set
            {
                this._SeqNum = value;
            }
        }


        /// <summary>
        /// Xác định định danh người dùng
        /// 
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
        public String Build()
        {
            
            return MessageRaw;
        }
    }
}
