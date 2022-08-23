using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IoTMessage;
namespace Client.View
{
    /// <summary>
    /// Interaction logic for Controller.xaml
    /// </summary>
    public partial class Controller : Window
    {
        public IoTMessageBase Message = new IoTMessageBase();
        public Controller()
        {
            InitializeComponent();

        }

        private void ClickBtnOK(object sender, RoutedEventArgs e)
        {
            if (tbDeviceID.Text != "" && tbHomeID.Text != "")
            {
                switch (cbDeviceType.Text)
                {
                    case "Fan":
                        Message.DeviceType = MessageType.FanController;
                        break;
                    case "Light":
                        Message.DeviceType = MessageType.LightController;
                        break;
                    case "Air Condition":
                        Message.DeviceType = MessageType.AirConditonController;
                        break;
                }
                Message.TargetID = tbHomeID.Text;
                if (cbAction.Text == "On")
                    Message.OnOff = 1;
                else Message.OnOff = 0;

            }
            else
                MessageBox.Show("Chưa nhập đủ thông tin");
            this.Close();
        }

    }
}
