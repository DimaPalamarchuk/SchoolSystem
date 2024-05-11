using SchoolSystem.Models;
using SchoolSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolSystem.ViewModels.Employee
{
    public class EmployeeSubjectsViewModel: ViewModelBase
    {
        // Fields
        private string _newSubjectName;
        private List<SubjectModel> _allSubjects;

        private SubjectRepository subjectRepository;

        // Properties
        public string NewSubjectName
        {
            get
            {
                return _newSubjectName;
            }
            set
            {
                _newSubjectName = value;
                OnPropertyChanged(nameof(NewSubjectName));
            }
        }

        public List<SubjectModel> AllSubjects
        {
            get
            {
                return _allSubjects;
            }
            set
            {
                _allSubjects = value;
                OnPropertyChanged(nameof(AllSubjects));
            }
        }

        public ICommand AddSubjectCommand { get; }
        public ICommand DeleteSubjectCommand { get; }

        public EmployeeSubjectsViewModel()
        {
            subjectRepository = new SubjectRepository();

            AllSubjects = [.. subjectRepository.GetAll().OrderBy(subject => subject.Name)];

            DeleteSubjectCommand = new ViewModelCommand(ExecuteDeleteSubjectCommand);
            AddSubjectCommand = new ViewModelCommand(ExecuteAddSubjectCommand, CanExecuteAddSubjectCommand);
        }

        private bool CanExecuteAddSubjectCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(NewSubjectName) && NewSubjectName.Length > 3)
            {
                return true;
            }

            return false;
        }

        private void ExecuteAddSubjectCommand(object obj)
        {
            subjectRepository.Add(NewSubjectName);

            NewSubjectName = "";
            AllSubjects = [.. subjectRepository.GetAll().OrderBy(subject => subject.Name)];
        }

        private void ExecuteDeleteSubjectCommand(object obj)
        {
            Guid subjectId = (Guid)obj;

            subjectRepository.Delete(subjectId);

            AllSubjects = [.. subjectRepository.GetAll().OrderBy(subject => subject.Name)];
        }
    }
}
