using SchoolSystem.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolSystem.Repositories
{
    public class BookRepository : RepositoryBase, IBookRepository
    {
        public List<BookModel> GetNotBorrowedBooksByStudentId(Guid studentId)
        {
            List<BookModel> books = new List<BookModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"SELECT b.BookID, b.Title
                      FROM Books b
                      LEFT JOIN BorrowedBooks bb ON b.BookID = bb.BookID AND bb.StudentID = @studentId
                      WHERE bb.StudentID IS NULL OR bb.StudentID != @studentId";
                command.Parameters.Add("@studentId", SqlDbType.UniqueIdentifier).Value = studentId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookModel book = new BookModel()
                        {
                            Id = (Guid)reader[0],
                            Name = (string)reader[1],
                        };
                        books.Add(book);
                    }
                }
            }
            return books;
        }


        public List<BookModel> GetBorrowedBooksByStudentId(Guid studentId)
        {
            List<BookModel> books = new List<BookModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"SELECT b.BookID, b.Title
                      FROM Books b
                      LEFT JOIN BorrowedBooks bb ON b.BookID = bb.BookID AND bb.StudentID = @studentId
                      WHERE bb.StudentID = @studentId";
                command.Parameters.Add("@studentId", SqlDbType.UniqueIdentifier).Value = studentId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookModel book = new BookModel()
                        {
                            Id = (Guid)reader[0],
                            Name = (string)reader[1],
                        };
                        books.Add(book);
                    }
                }
            }
            return books;
        }

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"SELECT BookID, Title
                      FROM Books;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookModel book = new BookModel()
                        {
                            Id = (Guid)reader[0],
                            Name = (string)reader[1],
                        };
                        books.Add(book);
                    }
                }
            }
            return books;
        }

        public void DeleteBook(Guid bookId) 
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"DELETE FROM BorrowedBooks WHERE BookID = @bookId;
                      DELETE FROM Books WHERE BookID = @bookId;";
                command.Parameters.Add("@bookId", SqlDbType.UniqueIdentifier).Value = bookId;
                command.ExecuteNonQuery();
            }
        }

        public void AddBook(string bookName)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"INSERT INTO Books VALUES (NEWID(), @bookName)";
                command.Parameters.Add("@bookName", SqlDbType.Text).Value = bookName;
                command.ExecuteNonQuery();
            }
        }

        public void BorrowBook(Guid bookId, Guid studentId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"INSERT INTO BorrowedBooks VALUES (NEWID(), @bookId, @studentId);";
                command.Parameters.Add("@bookId", SqlDbType.UniqueIdentifier).Value = bookId;
                command.Parameters.Add("@studentId", SqlDbType.UniqueIdentifier).Value = studentId;
                command.ExecuteNonQuery();
            }
        }

        public void ReturnBook(Guid bookId, Guid studentId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"DELETE FROM BorrowedBooks
                      WHERE BookID = @bookId AND StudentID = @studentId;";
                command.Parameters.Add("@bookId", SqlDbType.UniqueIdentifier).Value = bookId;
                command.Parameters.Add("@studentId", SqlDbType.UniqueIdentifier).Value = studentId;
                command.ExecuteNonQuery();
            }
        }
    }
}
