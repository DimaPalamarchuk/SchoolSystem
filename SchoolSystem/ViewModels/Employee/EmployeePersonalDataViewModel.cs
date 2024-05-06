using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.ViewModels.Employee
{
    public class EmployeePersonalDataViewModel : ViewModelBase
    {
        // Fields
        private EmployeeAccountModel _currentEmployeeAccount;

        // Properties
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

        public EmployeePersonalDataViewModel(EmployeeAccountModel currentEmployeeAccount)
        {
            CurrentEmployeeAccount = currentEmployeeAccount;
        }
    }
}
