using System.Configuration;
using System.Data;
using System.Windows;
using SchoolSystem.Views;
using SchoolSystem.Views.Student;

namespace SchoolSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e) 
        {
            var loginView = new LoginView();
            loginView.Show();
        }
    }
}
