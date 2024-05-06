using SchoolSystem.Models;
using SchoolSystem.Repositories;
using SchoolSystem.ViewModels.Student;
using SchoolSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolSystem.ViewModels.Employee
{
    public class EmployeeMainWindowViewModel : ViewModelBase
    {
        // Fields
        private ViewModelBase _currentChildView;
        private string _caption;
        private EmployeeAccountModel _currentEmployeeAccount;

        private IEmployeeAccountRepository employeeAccountRepository;

        // Properties
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public EmployeeAccountModel CurrentEmployeeAccount
        {
            get
            {
                return _currentEmployeeAccount;
            }
            set
            {
                _currentEmployeeAccount = value;
                OnPropertyChanged(nameof(CurrentEmployeeAccount));
            }
        }

        // Commands
        public ICommand ShowEmployeePersonalDataViewCommand { get; }
        public ICommand ShowEmployeeStudentsViewCommand { get; }
        public ICommand ShowEmployeeLibraryViewCommand { get; }
        public ICommand ExitCommand { get; }

        public EmployeeMainWindowViewModel()
        {
            employeeAccountRepository = new EmployeeAccountRepository();
            LoadCurrentUserData();

            // Initialize commands
            ShowEmployeePersonalDataViewCommand = new ViewModelCommand(ExecuteShowEmployeePersonalDataViewCommand);
            ShowEmployeeStudentsViewCommand = new ViewModelCommand(ExecuteShowEmployeeStudentsViewCommand);
            ShowEmployeeLibraryViewCommand = new ViewModelCommand(ExecuteShowEmployeeLibraryViewCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);

            // Default view
            ExecuteShowEmployeePersonalDataViewCommand(null);
        }

        private void LoadCurrentUserData()
        {
            CurrentEmployeeAccount = employeeAccountRepository.getByUsername(Thread.CurrentPrincipal.Identity.Name);
        }
        private void ExecuteShowEmployeePersonalDataViewCommand(object obj)
        {
            CurrentChildView = new EmployeePersonalDataViewModel(CurrentEmployeeAccount);
            Caption = "Personal data";
        }
        private void ExecuteShowEmployeeStudentsViewCommand(object obj)
        {
            CurrentChildView = new EmployeeStudentsViewModel();
            Caption = "Students";
        }
        private void ExecuteShowEmployeeLibraryViewCommand(object obj)
        {
            CurrentChildView = new EmployeeLibraryViewModel();
            Caption = "Library";
        }
        private void ExecuteExitCommand(object obj)
        {
            LoginView LoginView = new LoginView();
            LoginView.DataContext = new LoginViewModel();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = LoginView;
            Application.Current.MainWindow.Show();
            Thread.CurrentPrincipal = null;
        }
    }
}
