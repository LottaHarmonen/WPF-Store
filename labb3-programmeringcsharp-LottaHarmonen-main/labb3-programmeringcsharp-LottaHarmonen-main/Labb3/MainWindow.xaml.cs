using System.Windows;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            
            InitializeComponent(); 
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;

        }

        private void UserManager_CurrentUserChanged()
        {
            if (UserManager.IsAdminLoggedIn)
            {
                AdminTab.Visibility = Visibility.Visible;
                ShopTab.Visibility = Visibility.Collapsed;
                LoginTab.Visibility = Visibility.Collapsed;
                LoginViewI.Visibility = Visibility.Collapsed;
                AdminTab.IsSelected = true;
            }
            else if (UserManager.IsCustomerLoggedIn)
            {
                ShopTab.Visibility = Visibility.Visible;
                AdminTab.Visibility = Visibility.Collapsed;
                LoginTab.Visibility = Visibility.Collapsed;
                LoginViewI.Visibility = Visibility.Collapsed;
                ShopTab.IsSelected = true;
            }
            else
            {
                LoginTab.Visibility = Visibility.Visible;
                AdminTab.Visibility = Visibility.Collapsed;
                ShopTab.Visibility = Visibility.Collapsed;

                LoginViewI.Visibility = Visibility.Visible;
                LoginTab.IsSelected = true;
            }
        }
    }
}
