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

namespace Client.View
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public String Username="";
        public String Password="";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void ClickBtnLogin(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text == "" || tbPassword.Password == "")
                MessageBox.Show("Nhập thiếu thông tin");
            else
            {
                Username = tbUserName.Text;
                Password = tbPassword.Password;
                this.Close();
            }
            
        }
    }
}
