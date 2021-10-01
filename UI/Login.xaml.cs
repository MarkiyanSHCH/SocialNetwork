using MongoDB.Bson;
using MongoDB.Controllers;
using MongoDB.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        BL bl = new BL();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            bool isTrue = false;
            foreach (var user in bl.users)
            {
                if (user.Email == txtUserId.Text)
                {
                    if(user.Password == txtPassword.Password)
                    {
                        MainWindow mw = new MainWindow(user);
                        mw.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Error: incorrect email or password");
                    }
                }
                else
                {
                    isTrue = true;
                }
            }
        }
    }
}
