using System;
using System.Collections.Generic;
namespace IoTMessage
{
    public class IoTMessageBase
    {
        public static String IOTBegin = "IOT=Begin";
        public static String IOTEnd = "IOT=End";
        public String MessageRaw { get; set; }
        /// <summary>
        /// Xác định loại Message
        /// Tag: 1
        /// </summary>
        protected int _MessageType = -1;
        /// <summary>
        /// Xác định thứ tự message
        /// tag=2
        /// </summary>
        protected long _SeqNum = -1;
        /// <summary>
        /// Xác định ID người dùng
        /// Tag=3
        /// </summary>
        protected String _Account = "";
        /// <summary>
        /// Xác định ID nhà cần điều khiển, 
        /// Tag=4
        /// </summary>
        protected String _HomeID = "";
        /// <summary>
        /// Xác định trạng thái thiết bị
        /// Tag=5
        /// </summary>
        protected int _Status = -1;
        /// <summary>
        /// Lệnh điều khiển 
        /// Tag=6
        /// </summary>
        protected int _OnOff = -1;
        /// <summary>
        /// Xác định loại thiết bị
        /// Tag=7
        /// </summary>
        protected int _DeviceType = -1;
        /// <summary>
        /// Xác định giá trị nhiệt độ điều hòa
        /// Tag=8
        /// </summary>
        protected int _Degree = -1;
        /// <summary>
        /// Xác định độ mạnh của quạt
        /// Tag=9
        /// </summary>
        protected int _FanLevel = -1;
        /// <summary>
        /// Xác định ID thiết bị
        /// </summary>
        protected string _DeviceID = "";
        /// <summary>
        /// Kiểm tra chế độ của Resend Request
        /// tag=10
        /// </summary>
        protected int _GapFill = -1;
        /// <summary>
        /// Đánh dấu message bắt đầu của resend
        /// tag=11
        /// </summary>
        protected long _BeginSendNo = -1;
        /// <summary>
        /// Đánh dấu Message bắt đầu của 
        /// tag=12
        /// </summary>
        protected long _EndSendNo = -1;
        /// <summary>
        /// Mật khẩu, dùng trong message login
        /// tag=13
        /// </summary>
        protected String _Password = "";
        /// <summary>
        /// ID bên nhận, Tag=14
        /// </summary>
        protected String _TargetID = "";
        /// <summary>
        /// ID bên gửi, Tag=15
        /// </summary>
        protected String _SenderID = "";
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
            }
            set
            {
                _Account = value;
            }
        }
        public String Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }    
        /// <summary>
        /// Xác định trạng thái bật tắt của thiết bị
        /// </summary>
        public int OnOff
        {
            get
            {
                return _OnOff;
            }
            set
            {
                _OnOff = value;
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
        /// <summary>
        /// Xác định loại thiết bị
        /// </summary>
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
        /// <summary>
        /// Định danh bên nhận
        /// Tag=14
        /// </summary>
        public String TargetID
        {
            get
            {
                return _TargetID;
            }
            set
            {
                _SenderID=value;
            }
        }
        /// <summary>
        /// ID bên gửi, Tag=15
        /// </summary>
        public String SenderID
        {
            get
            {
                return _SenderID;
            }
            set
            {
                _SenderID = value;
            }
        }
        public String Build()
        {
            //Degree-Fanlevel-DeviceID-Gapfill-BeginSeqNo-EndSendNo
            MessageRaw = "";
            MessageRaw += IOTBegin+"&";
            if (_MessageType != -1) MessageRaw += "1=" + _MessageType.ToString() + "&";
            if (_SeqNum != -1) MessageRaw += "2=" + _SeqNum.ToString() + "&";
            if (_Account != "") MessageRaw += "3=" + _Account + "&";
            if (_HomeID != "") MessageRaw += "4=" + _HomeID + "&";
            if (_Status != -1) MessageRaw += "5=" + _Status.ToString() + "&";
            if (_OnOff != -1) MessageRaw += "6=" + _OnOff.ToString() + "&";
            if (DeviceType != -1) MessageRaw += "7=" + DeviceType.ToString() + "&";
            if (_Degree != -1) MessageRaw += "8=" + _Degree.ToString() + "&";
            if (_FanLevel != -1) MessageRaw += "9=" + _FanLevel.ToString() + "&";
            if (_GapFill != -1) MessageRaw += "10=" + _GapFill.ToString() + "&";
            if (_BeginSendNo != -1) MessageRaw += "11=" + _BeginSendNo.ToString() + "&";
            if (_EndSendNo != -1) MessageRaw += "12=" + _EndSendNo.ToString() + "&";
            if (_Password != "") MessageRaw += "13=" + _Password + "&";
            if (_TargetID != "") MessageRaw += "14=" + _TargetID+"&";
            if (_SenderID != "") MessageRaw += "15=" + _SenderID +"&";
            MessageRaw += IOTEnd;
            return MessageRaw;
        }
        public IoTMessageBase() { }
        public IoTMessageBase(String a)
        {
            MessageRaw = a;
            if (a.IndexOf(IOTBegin)>=0)
            {
                String[] a1 = a.Split('&');
                foreach (String tag in a1)
                {
                    String[] a2 = tag.Split('=');
                    switch (a2[0])
                    {
                        case "1":
                            _MessageType = int.Parse(a2[a2.Length-1]);
                            break;
                        case "2":
                            _SeqNum= int.Parse(a2[a2.Length - 1]);
                            break;
                        case "3":
                            _Account= a2[a2.Length - 1];
                            break;
                        case "4":
                            _HomeID= a2[a2.Length - 1];
                            break;
                        case "5":
                            _Status= int.Parse(a2[a2.Length - 1]);
                            break;
                        case "6":
                            _OnOff = int.Parse(a2[a2.Length - 1]);
                            break;
                        case "7":
                            DeviceType = int.Parse(a2[a2.Length - 1]);
                            break;
                        case "8":
                            _Degree = int.Parse(a2[a2.Length - 1]);
                            break;
                        case "9":
                            _FanLevel = int.Parse(a2[a2.Length - 1]);
                            break;
                        case "10":
                            _GapFill = int.Parse(a2[a2.Length - 1]);
                            break;
                        case "11":
                            _BeginSendNo = int.Parse(a2[a2.Length - 1]);
                            break;
                        case "12":
                            _EndSendNo = int.Parse(a2[a2.Length - 1]);
                            break;
                        case "13":
                            _Password = a2[a2.Length - 1];
                            break;
                        case "14":
                            _TargetID = a2[a2.Length - 1];
                            break;
                        case "15":
                            _SenderID = a2[a2.Length - 1];
                            break;
                    }
                }    
            }    
        }
            
    }
}
