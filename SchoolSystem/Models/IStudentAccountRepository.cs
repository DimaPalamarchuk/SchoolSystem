using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Models
{
    public interface IStudentAccountRepository
    {
        StudentAccountModel getByUsername(string username);
        List<StudentAccountModel> getAllStudentAccounts();
        void AddStudent(string firstName, string secondName, string username, string password);
        void DeleteStudent(Guid studentId);
    }
}
