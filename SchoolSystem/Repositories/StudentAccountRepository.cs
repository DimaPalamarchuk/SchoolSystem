using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Models;

namespace SchoolSystem.Repositories
{
    public class StudentAccount : RepositoryBase, IStudentAccountRepository
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
    }
}
