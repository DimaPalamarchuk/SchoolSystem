using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Repositories
{
    public class EmployeeAccountRepository : RepositoryBase, IEmployeeAccountRepository
    {
        public EmployeeAccountModel getByUsername(string username)
        {
            EmployeeAccountModel employeeAccount = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"SELECT u.UserID, e.EmployeeID, u.FirstName, u.LastName, u.Username 
                      FROM Users u
                      INNER JOIN Employees e ON u.UserID = e.UserID
                      WHERE u.Username = @username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employeeAccount = new EmployeeAccountModel()
                        {
                            UserId = (Guid)reader[0],
                            EmployeeId = (Guid)reader[1],
                            FirstName = (string)reader[2],
                            LastName = (string)reader[3],
                            Username = (string)reader[4],
                        };
                    }
                }
            }
            return employeeAccount;
        }
    }
}
