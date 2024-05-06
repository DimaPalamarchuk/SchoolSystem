using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Models
{
    public interface IBookRepository
    {
        List<BookModel> GetNotBorrowedBooksByStudentId(Guid studentId);
        List<BookModel> GetBorrowedBooksByStudentId(Guid studentId);
        void ReturnBook(Guid bookId, Guid StudentId);
        void BorrowBook(Guid bookId, Guid StudentId);
    }
}
