using FontAwesome.Sharp;
using SchoolSystem.Models;
using SchoolSystem.Repositories;
using SchoolSystem.Views;
using SchoolSystem.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolSystem.ViewModels.Student
{
    public class StudentMainWindowViewModel : ViewModelBase
    {
        // Fields
        private ViewModelBase _currentChildView;
        private string _caption;
        private StudentAccountModel _currentStudentAccount;

        private IStudentAccountRepository studentAccountRepository;

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

        public StudentAccountModel CurrentStudentAccount
        {
            get
            {
                return _currentStudentAccount;
            }
            set
            {
                _currentStudentAccount = value;
                OnPropertyChanged(nameof(CurrentStudentAccount));
            }
        }

        //--> Commands
        public ICommand ShowStudentPersonalDataViewCommand { get; }
        public ICommand ShowStudentGradesViewCommand { get; }
        public ICommand ShowStudentMyBooksViewCommand { get; }
        public ICommand ShowStudentLibraryCatalogViewCommand { get; }
        public ICommand ExitCommand { get; }

        public StudentMainWindowViewModel()
        {
            studentAccountRepository = new StudentAccount();
            LoadCurrentUserData();

            // Initialize commands
            ShowStudentPersonalDataViewCommand = new ViewModelCommand(ExecuteShowStudentPersonalDataViewCommand);
            ShowStudentGradesViewCommand = new ViewModelCommand(ExecuteShowStudentGradesViewCommand);
            ShowStudentMyBooksViewCommand = new ViewModelCommand(ExecuteShowStudentMyBooksViewCommand);
            ShowStudentLibraryCatalogViewCommand = new ViewModelCommand(ExecuteShowStudentLibraryCatalogViewCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);

            // Default view
            ExecuteShowStudentPersonalDataViewCommand(null);
        }

        private void LoadCurrentUserData()
        {
            CurrentStudentAccount = studentAccountRepository.getByUsername(Thread.CurrentPrincipal.Identity.Name);
        }

        private void ExecuteShowStudentPersonalDataViewCommand(object obj)
        {
            CurrentChildView = new StudentPersonalDataViewModel(CurrentStudentAccount);
            Caption = "Personal data";
        }
        private void ExecuteShowStudentGradesViewCommand(object obj)
        {
            CurrentChildView = new StudentGradesViewModel(CurrentStudentAccount);
            Caption = "Grades";
        }
        private void ExecuteShowStudentMyBooksViewCommand(object obj)
        {
            CurrentChildView = new StudentMyBooksViewModel(CurrentStudentAccount);
            Caption = "My Books";
        }
        private void ExecuteShowStudentLibraryCatalogViewCommand(object obj)
        {
            CurrentChildView = new StudentLibraryCatalogViewModel(CurrentStudentAccount);
            Caption = "Library Catalog";
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
