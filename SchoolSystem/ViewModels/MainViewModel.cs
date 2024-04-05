using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolSystem.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private ViewModelBase _currentChildView;
        private string _caption;

        //Properties
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

        //--> Commands
        public ICommand ShowPersonalDataViewCommand { get; }
        public ICommand ShowGradesViewCommand { get; }
        public ICommand ShowMyBooksViewCommand { get; }
        public ICommand ShowLibraryCatalogViewCommand { get; }

        public MainViewModel()
        {
            //Initialize commands
            ShowPersonalDataViewCommand = new ViewModelCommand(ExecuteShowPersonalDataViewCommand);
            ShowGradesViewCommand = new ViewModelCommand(ExecuteShowGradesViewCommand);
            ShowMyBooksViewCommand = new ViewModelCommand(ExecuteShowMyBooksViewCommand);
            ShowLibraryCatalogViewCommand = new ViewModelCommand(ExecuteShowLibraryCatalogViewCommand);

            //Default view
            ExecuteShowPersonalDataViewCommand(null);
        }

        private void ExecuteShowPersonalDataViewCommand(object obj)
        {
            CurrentChildView = new PersonalDataViewModel();
            Caption = "Personal data";
        }
        private void ExecuteShowGradesViewCommand(object obj)
        {
            CurrentChildView = new GradesViewModel();
            Caption = "Grades";
        }
        private void ExecuteShowMyBooksViewCommand(object obj)
        {
            CurrentChildView = new MyBooksViewModel();
            Caption = "My Books";
        }
        private void ExecuteShowLibraryCatalogViewCommand(object obj)
        {
            CurrentChildView = new LibraryCatalogViewModel();
            Caption = "Library Catalog";
        }
    }
}
