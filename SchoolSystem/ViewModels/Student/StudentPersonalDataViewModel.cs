using SchoolSystem.Models;
using SchoolSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolSystem.ViewModels.Student
{
    public class StudentPersonalDataViewModel: ViewModelBase
    {
        // Fields
        private StudentAccountModel _currentStudentAccount;

        // Properties
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

        public StudentPersonalDataViewModel(StudentAccountModel currentStudentAccount)
        {
            CurrentStudentAccount = currentStudentAccount;
        }
    }
}
