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

        private BookRepository bookRepository;

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
            bookRepository = new BookRepository();

            CurrentStudentAccount = currentStudentAccount;

            NotBorrowedBooks = [.. bookRepository.GetNotBorrowedBooksByStudentId(CurrentStudentAccount.StudentId).OrderBy(book => book.Name)];

            BorrowBookCommand = new ViewModelCommand(ExecuteBorrowBookCommand);
        }

        private void ExecuteBorrowBookCommand(object obj)
        {
            Guid bookId = (Guid)obj;

            bookRepository.BorrowBook(bookId, CurrentStudentAccount.StudentId);

            NotBorrowedBooks = [.. bookRepository.GetNotBorrowedBooksByStudentId(CurrentStudentAccount.StudentId).OrderBy(book => book.Name)];
        }
    }
}
