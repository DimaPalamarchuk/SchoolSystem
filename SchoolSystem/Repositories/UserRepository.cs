using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public bool isAuthenticatedUser(NetworkCredential credential)
        {
            bool validUser;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Users WHERE username = @username and password = @password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }

            return validUser;
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText =
                    @"SELECT u.UserID, u.Username, r.RoleName
                      FROM Users u
                      INNER JOIN Roles r ON u.RoleID = r.RoleID
                      WHERE u.Username = @username;";

                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = (Guid)reader[0],
                            Username = (string)reader[1],
                            Role = (string)reader[2]
                        };
                    }
                }
            }
            return user;
        }
    }
}
