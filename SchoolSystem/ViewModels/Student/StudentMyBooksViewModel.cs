using SchoolSystem.Models;
using SchoolSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolSystem.ViewModels.Student
{
    class StudentMyBooksViewModel : ViewModelBase
    {
        // Fields
        private StudentAccountModel _currentStudentAccount;
        private List<BookModel> _borrowedBooks;

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

        public List<BookModel> BorrowedBooks
        {
            get
            {
                return _borrowedBooks;
            }
            set
            {
                _borrowedBooks = value;
                OnPropertyChanged(nameof(BorrowedBooks));
            }
        }

        public ICommand ReturnBookCommand { get; }

        public StudentMyBooksViewModel(StudentAccountModel currentStudentAccount)
        {
            bookRepository = new BookRepository();

            CurrentStudentAccount = currentStudentAccount;

            BorrowedBooks = [.. bookRepository.GetBorrowedBooksByStudentId(CurrentStudentAccount.StudentId).OrderBy(book => book.Name)];

            ReturnBookCommand = new ViewModelCommand(ExecuteReturnBookCommand);
        }

        private void ExecuteReturnBookCommand(object obj)
        {
            Guid bookId = (Guid)obj;

            bookRepository.ReturnBook(bookId, CurrentStudentAccount.StudentId);
            BorrowedBooks = [.. bookRepository.GetBorrowedBooksByStudentId(CurrentStudentAccount.StudentId).OrderBy(book => book.Name)];
        }
    }
}
