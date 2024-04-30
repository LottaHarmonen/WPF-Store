using Labb3ProgTemplate.Managerrs;
using System.Windows;

namespace Labb3ProgTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            UserManager.LoadUsersFromFile();
            ProductManager.LoadProductsFromFile();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            UserManager.SaveUsersToFile();
            ProductManager.SaveProductsToFile();


            base.OnExit(e);
        }



    }

  
}
