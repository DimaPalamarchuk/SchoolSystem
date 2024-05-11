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
    public class StudentGradesViewModel: ViewModelBase
    {
        // Fields
        private StudentAccountModel _currentStudentAccount;
        private List<GradeModel> _studentGrades;

        private GradeRepository gradeRepository;

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

        public List<GradeModel> StudentGrades
        {
            get
            {
                return _studentGrades;
            }
            set
            {
                _studentGrades = value;
                OnPropertyChanged(nameof(StudentGrades));
            }
        }

        public StudentGradesViewModel(StudentAccountModel currentStudentAccount)
        {
            gradeRepository = new GradeRepository();

            CurrentStudentAccount = currentStudentAccount;
            StudentGrades = [.. gradeRepository.GetStudentGradesByStudentId(CurrentStudentAccount.StudentId).OrderBy(grade => grade.SubjectName)];
        }
    }
}
