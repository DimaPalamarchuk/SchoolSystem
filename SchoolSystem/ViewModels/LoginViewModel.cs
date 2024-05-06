using SchoolSystem.Models;
using SchoolSystem.Repositories;
using SchoolSystem.ViewModels.Employee;
using SchoolSystem.ViewModels.Student;
using SchoolSystem.Views.Employee;
using SchoolSystem.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolSystem.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        // Properties
        public string Username { 
            get 
            { 
                return _username;
            } 
            
            set 
            { 
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }

            set 
            { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage { 
            get 
            {
                return _errorMessage;
            } 
            
            set
            { 
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible { 
            get 
            { 
                return _isViewVisible;
            }

            set 
            { 
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // Commands
        public ICommand LoginCommand { get; }

        // Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;

            if (string.IsNullOrWhiteSpace(Username) || Password == null)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }

            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isAuthenticatedUser = userRepository.isAuthenticatedUser(new NetworkCredential(Username, Password));
            var currentUser = userRepository.GetByUsername(Username);

            if (isAuthenticatedUser && currentUser != null)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                if (currentUser.Role == "student")
                {
                    StudentMainWindowView studentMainWindowView = new StudentMainWindowView();
                    studentMainWindowView.DataContext = new StudentMainWindowViewModel();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = studentMainWindowView;
                    Application.Current.MainWindow.Show();
                } else if (currentUser.Role == "employee")
                {
                    EmployeeMainWindowView employeeMainWindowView = new EmployeeMainWindowView();
                    employeeMainWindowView.DataContext = new EmployeeMainWindowViewModel();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = employeeMainWindowView;
                    Application.Current.MainWindow.Show();
                }
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }
    }
}
