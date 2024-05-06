using SchoolSystem.Models;
using SchoolSystem.Repositories;
using SchoolSystem.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolSystem.ViewModels.Student
{
    class StudentLibraryCatalogViewModel : ViewModelBase
    {
        // Fields
        private StudentAccountModel _currentStudentAccount;
        private List<BookModel> _notBorrowedBooks;

        private BookRepository studentBookRepository;

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

        public List<BookModel> NotBorrowedBooks
        {
            get
            {
                return _notBorrowedBooks;
            }
            set
            {
                _notBorrowedBooks = value;
                OnPropertyChanged(nameof(NotBorrowedBooks));
            }
        }

        public ICommand BorrowBookCommand { get; }

        public StudentLibraryCatalogViewModel(StudentAccountModel currentStudentAccount)
        {
            studentBookRepository = new BookRepository();

            CurrentStudentAccount = currentStudentAccount;

            NotBorrowedBooks = studentBookRepository.GetNotBorrowedBooksByStudentId(CurrentStudentAccount.StudentId);

            BorrowBookCommand = new ViewModelCommand(ExecuteBorrowBookCommand);
        }

        private void ExecuteBorrowBookCommand(object obj)
        {
            Guid bookId = (Guid)obj;

            studentBookRepository.BorrowBook(bookId, CurrentStudentAccount.StudentId);
            NotBorrowedBooks = studentBookRepository.GetNotBorrowedBooksByStudentId(CurrentStudentAccount.StudentId);
        }
    }
}
