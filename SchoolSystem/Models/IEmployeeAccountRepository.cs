using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Models
{
    public interface IEmployeeAccountRepository
    {
        EmployeeAccountModel getByUsername(string username);
    }
}
