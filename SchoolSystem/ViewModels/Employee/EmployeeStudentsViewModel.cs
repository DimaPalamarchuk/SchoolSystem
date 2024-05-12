using SchoolSystem.Models;
using SchoolSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolSystem.ViewModels.Employee
{
    public class EmployeeStudentsViewModel : ViewModelBase
    {
        // Fields
        private string _newStudentFirstName;
        private string _newStudentLastName;
        private string _newStudentIndexNo;
        private string _newStudentPassword;
        private string _errorMessage;
        private SubjectModel _newGradeSubject;
        private ComboBoxItem _newGrade1;
        private ComboBoxItem _newGrade2;
        private List<StudentAccountModel> _allStudentAccounts;
        private StudentAccountModel _selectedStudentAccount;
        private List<GradeModel> _selectedStudentGrades;
        private List<BookModel> _selectedStudentBorrowedBooks;
        private List<BookModel> _selectedStudentNotBorrowedBooks;
        private BookModel _selectedStudentNotBorrowedBook;
        private List<SubjectModel> _allSubjects;

        private StudentAccountRepository studentAccountRepository;
        private GradeRepository gradeRepository;
        private BookRepository bookRepository;
        private SubjectRepository subjectRepository;

        // Properties
        public string NewStudentFirstName {
            get
            {
                return _newStudentFirstName;
            }
            set
            {
                _newStudentFirstName = value;
                OnPropertyChanged(nameof(NewStudentFirstName));
            }
        }

        public string NewStudentLastName
        {
            get
            {
                return _newStudentLastName;
            }
            set
            {
                _newStudentLastName = value;
                OnPropertyChanged(nameof(NewStudentLastName));
            }
        }

        public string NewStudentIndexNo
        {
            get
            {
                return _newStudentIndexNo;
            }
            set
            {
                _newStudentIndexNo = value;
                OnPropertyChanged(nameof(NewStudentIndexNo));
            }
        }

        public string NewStudentPassword
        {
            get
            {
                return _newStudentPassword;
            }
            set
            {
                _newStudentPassword = value;
                OnPropertyChanged(nameof(NewStudentPassword));
            }
        }

        public string ErrorMessage
        {
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

        public SubjectModel NewGradeSubject
        {
            get
            {
                return _newGradeSubject;
            }
            set
            {
                _newGradeSubject = value;
                NotifyPropertyChanged(nameof(NewGradeSubject));
            }
        }

        public ComboBoxItem NewGrade1
        {
            get
            {
                return _newGrade1;
            }
            set
            {
                _newGrade1 = value;
                NotifyPropertyChanged(nameof(NewGrade1));
            }
        }

        public ComboBoxItem NewGrade2
        {
            get
            {
                return _newGrade2;
            }
            set
            {
                _newGrade2 = value;
                NotifyPropertyChanged(nameof(NewGrade2));
            }
        }

        public List<StudentAccountModel> AllStudentAccounts
        {
            get
            {
                return _allStudentAccounts;
            }
            set
            {
                _allStudentAccounts = value; 
                OnPropertyChanged(nameof(AllStudentAccounts));
            }
        }

        public StudentAccountModel SelectedStudentAccount
        {
            get
            {
                return _selectedStudentAccount;
            }
            set
            {
                _selectedStudentAccount = value;
                NotifyPropertyChanged(nameof(SelectedStudentAccount));

                if (SelectedStudentAccount != null) { 
                    UpdateSelectedStudentGrades();
                    UpdateSelectedStudentBorrowedBooks();
                    UpdateSelectedStudentNotBorrowedBooks();
                }
            }
        }

        public List<GradeModel> SelectedStudentGrades
        {
            get
            {
                return _selectedStudentGrades;
            }
            set
            {
                _selectedStudentGrades = value;
            }
        }

        public List<BookModel> SelectedStudentBorrowedBooks
        {
            get
            {
                return _selectedStudentBorrowedBooks;
            }
            set
            {
                _selectedStudentBorrowedBooks = value;
            }
        }

        public List<BookModel> SelectedStudentNotBorrowedBooks
        {
            get
            {
                return _selectedStudentNotBorrowedBooks;
            }
            set
            {
                _selectedStudentNotBorrowedBooks = value;
            }
        }

        public BookModel SelectedStudentNotBorrowedBook
        {
            get
            {
                return _selectedStudentNotBorrowedBook;
            }
            set
            {
                _selectedStudentNotBorrowedBook = value;
                NotifyPropertyChanged(nameof(SelectedStudentNotBorrowedBook));
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
            }
        }

        // Commands
        public ICommand AddStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand DeleteGradeCommand { get; }
        public ICommand AddGradeCommand { get; }
        public ICommand ReturnBookCommand { get; }
        public ICommand BorrowBookCommand { get; }

        public EmployeeStudentsViewModel() {
            studentAccountRepository = new StudentAccountRepository();
            gradeRepository = new GradeRepository();
            bookRepository = new BookRepository();
            subjectRepository = new SubjectRepository();

            AllStudentAccounts = [.. studentAccountRepository.getAllStudentAccounts().OrderBy(student => student.IndexNo)];
            SelectedStudentAccount = AllStudentAccounts[0];
            AllSubjects = [.. subjectRepository.GetAll().OrderBy(subject => subject.Name)];

            UpdateSelectedStudentGrades();
            UpdateSelectedStudentBorrowedBooks();
            UpdateSelectedStudentNotBorrowedBooks();

            DeleteStudentCommand = new ViewModelCommand(ExecuteDeleteStudentCommand);
            AddStudentCommand = new ViewModelCommand(ExecuteAddStudentCommand, CanExecuteAddStudentCommand);
            DeleteGradeCommand = new ViewModelCommand(ExecuteDeleteGradeCommand);
            AddGradeCommand = new ViewModelCommand(ExecuteAddGradeCommand, CanExecuteAddGradeCommand);
            ReturnBookCommand = new ViewModelCommand(ExecuteReturnBookCommand);
            BorrowBookCommand = new ViewModelCommand(ExecuteBorrowBookCommand, CanExecuteBorrowBookCommand);
        }

        private void UpdateSelectedStudentGrades()
        {

            SelectedStudentGrades = [.. gradeRepository.GetStudentGradesByStudentId(SelectedStudentAccount.StudentId).OrderBy(grade => grade.SubjectName)];
            OnPropertyChanged(nameof(SelectedStudentGrades));
        }

        private void UpdateSelectedStudentBorrowedBooks()
        {
            SelectedStudentBorrowedBooks = [.. bookRepository.GetBorrowedBooksByStudentId(SelectedStudentAccount.StudentId).OrderBy(book => book.Name)];
            OnPropertyChanged(nameof(SelectedStudentBorrowedBooks));
        }

        private void UpdateSelectedStudentNotBorrowedBooks()
        {
            SelectedStudentNotBorrowedBooks = [.. bookRepository.GetNotBorrowedBooksByStudentId(SelectedStudentAccount.StudentId).OrderBy(book => book.Name)];
            OnPropertyChanged(nameof(SelectedStudentNotBorrowedBooks));
        }

        private bool CanExecuteAddStudentCommand(object obj)
        {
            bool validData = false;

            if (!string.IsNullOrWhiteSpace(NewStudentFirstName) &&
                !string.IsNullOrWhiteSpace(NewStudentLastName) &&
                !string.IsNullOrWhiteSpace(NewStudentIndexNo) &&
                !string.IsNullOrWhiteSpace(NewStudentPassword))
            {
                validData = true;
            }

            return validData;
        }

        private void ExecuteAddStudentCommand(object obj)
        {
            StudentAccountModel newStudent = studentAccountRepository.getByUsername(NewStudentIndexNo);

            if (newStudent == null)
            {
                studentAccountRepository.AddStudent(NewStudentFirstName, NewStudentLastName, NewStudentIndexNo, NewStudentPassword);
                AllStudentAccounts = [.. studentAccountRepository.getAllStudentAccounts().OrderBy(student => student.IndexNo)];
                SelectedStudentAccount = AllStudentAccounts.SingleOrDefault(studentAccount => studentAccount.IndexNo == NewStudentIndexNo);

                ErrorMessage = null;
                NewStudentFirstName = null;
                NewStudentLastName = null;
                NewStudentIndexNo = null;
                NewStudentPassword = null;
            }
            else
            {
                ErrorMessage = "A student with this Index no already exists";
            }
        }

        private void ExecuteDeleteStudentCommand(object obj)
        {
            studentAccountRepository.DeleteStudent(SelectedStudentAccount.StudentId);

            AllStudentAccounts = [.. studentAccountRepository.getAllStudentAccounts().OrderBy(student => student.IndexNo)];
            SelectedStudentAccount = AllStudentAccounts[0];
        }

        private void ExecuteDeleteGradeCommand(object obj)
        {
            Guid gradeId = (Guid)obj;

            gradeRepository.DeleteGrade(gradeId);
            UpdateSelectedStudentGrades();
        }

        private bool CanExecuteAddGradeCommand(object obj)
        {
            bool validData = false;

            if (NewGradeSubject != null && NewGrade1 != null && NewGrade1.Content != "")
            {
                validData = true;
            }

            return validData;
        }

        private void ExecuteAddGradeCommand(object obj)
        {
            int? grade2 = NewGrade2 != null && NewGrade2.Content != "" ? Convert.ToInt32(NewGrade2.Content) : null;

            gradeRepository.AddGrade(SelectedStudentAccount.StudentId, NewGradeSubject.Name, Convert.ToInt32(NewGrade1.Content), grade2);
            UpdateSelectedStudentGrades();

            NewGradeSubject = null;
            NewGrade1 = null;
            NewGrade2 = null;
        }

        private void ExecuteReturnBookCommand(object obj)
        {
            Guid bookId = (Guid)obj;

            bookRepository.ReturnBook(bookId, SelectedStudentAccount.StudentId);
            UpdateSelectedStudentBorrowedBooks();
            UpdateSelectedStudentNotBorrowedBooks();
        }

        private bool CanExecuteBorrowBookCommand(object obj)
        {
            if(SelectedStudentNotBorrowedBook != null)
            {
                return true;
            }

            return false;
        }

        private void ExecuteBorrowBookCommand(object obj)
        {
            Guid bookId = (Guid)obj;

            bookRepository.BorrowBook(bookId, SelectedStudentAccount.StudentId);
            UpdateSelectedStudentNotBorrowedBooks();
            UpdateSelectedStudentBorrowedBooks();
        }
    }
}
