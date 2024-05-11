using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolSystem.Repositories
{
    public class StudentAccountRepository : RepositoryBase, IStudentAccountRepository
    {
        public StudentAccountModel getByUsername(string username)
        {
            StudentAccountModel studentAccount = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText =
                    @"SELECT u.UserID, s.StudentID, u.FirstName, u.LastName, u.Username 
                      FROM Users u
                      INNER JOIN Students s ON u.UserID = s.UserID
                      WHERE u.Username = @username";

                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        studentAccount = new StudentAccountModel()
                        {
                            UserId = (Guid)reader[0],
                            StudentId = (Guid)reader[1],
                            FirstName = (string)reader[2],
                            LastName = (string)reader[3],
                            IndexNo = (string)reader[4],
                        };
                    }
                }
            }
            return studentAccount;
        }

        public List<StudentAccountModel> getAllStudentAccounts()
        {
            List<StudentAccountModel> allStudentAccounts = new List<StudentAccountModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText =
                    @"SELECT u.UserID, s.StudentID, u.FirstName, u.LastName, u.Username 
                      FROM Users u
                      INNER JOIN Students s ON u.UserID = s.UserID";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StudentAccountModel studentAccount = new StudentAccountModel()
                        {
                            UserId = (Guid)reader[0],
                            StudentId = (Guid)reader[1],
                            FirstName = (string)reader[2],
                            LastName = (string)reader[3],
                            IndexNo = (string)reader[4],
                        };

                        allStudentAccounts.Add(studentAccount);
                    }
                }
            }

            return allStudentAccounts;
        }

        public void AddStudent(string firstName, string secondName, string username, string password)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"DECLARE @userId UNIQUEIDENTIFIER
                      SET @userId = NEWID()
                      INSERT INTO Users VALUES (@userId, @username, @password, @firstName, @secondName, (SELECT RoleID FROM Roles WHERE RoleName = 'Student'))
                      INSERT INTO Students VALUES (@UserId, @UserId)";
                command.Parameters.Add("@firstName", SqlDbType.Text).Value = firstName;
                command.Parameters.Add("@secondName", SqlDbType.Text).Value = secondName;
                command.Parameters.Add("@username", SqlDbType.Text).Value = username;
                command.Parameters.Add("@password", SqlDbType.Text).Value = password;
                command.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(Guid studentId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"DELETE FROM BorrowedBooks WHERE StudentID = @studentId
                      DELETE FROM Grades WHERE StudentID = @studentId
                      DELETE FROM Students WHERE StudentID = @studentId
                      DELETE FROM Users WHERE UserID = @studentId";
                command.Parameters.Add("@studentId", SqlDbType.UniqueIdentifier).Value = studentId;
                command.ExecuteNonQuery();
            }
        }
    }
}
