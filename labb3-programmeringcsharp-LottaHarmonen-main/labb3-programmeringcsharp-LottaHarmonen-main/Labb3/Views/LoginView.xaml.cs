using System;
using System.Windows;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;

        }


        private void UserManager_CurrentUserChanged()
        {
            //töm box
            LoginName.Clear();
            LoginPwd.Clear();

        }

        private void LoginBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string text = LoginName.Text;
            string password = LoginPwd.Password;

            foreach (User user in UserManager.Users)
            {
                if (user.Name == text)
                {
                    bool Authenticated = user.Authenticate(password);
                    if (Authenticated)
                    {
                        UserManager.ChangeCurrentUser(user.Name, user.Password, user.Type);
                        return;
                    }
                    else if (Authenticated == false)
                    {
                        LoginFailed.IsOpen = false;
                    }
                }
            }
            LoginFailed.IsOpen = true;
            LoginName.Clear();
            LoginPwd.Clear();
        }

        private void RegisterAdminBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            string username = RegisterName.Text;
            string password = RegisterPwd.Password;

            UserTypes usertype = UserTypes.Admin;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            UserManager.Register(username, password, usertype);
            LoginPopup.IsOpen = true;
            RegisterName.Clear();
            RegisterPwd.Clear();
        }

        private void RegisterCustomerBtn_OnClickmerBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterName.Text;
            string password = RegisterPwd.Password;

            UserTypes usertype = UserTypes.Customer;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            UserManager.Register(username, password, usertype);
            LoginPopup.IsOpen = true;
            RegisterName.Clear();
            RegisterPwd.Clear();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            LoginPopup.IsOpen = false;

        }


        private void LogInFailedBtn_OnClick(object sender, RoutedEventArgs e)
        {
            LoginFailed.IsOpen = false;
        }


    }
}
