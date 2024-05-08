using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolSystem.Models
{
    public interface IBookRepository
    {
        List<BookModel> GetNotBorrowedBooksByStudentId(Guid studentId);
        List<BookModel> GetBorrowedBooksByStudentId(Guid studentId);
        List<BookModel> GetAllBooks();
        void DeleteBook(Guid bookId);
        void AddBook(string bookName);
        void ReturnBook(Guid bookId, Guid StudentId);
        void BorrowBook(Guid bookId, Guid StudentId);
    }
}
