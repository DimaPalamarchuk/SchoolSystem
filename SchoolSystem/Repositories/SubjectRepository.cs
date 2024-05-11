using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Repositories
{
    public class SubjectRepository : RepositoryBase, ISubjectRepository
    {
        public List<SubjectModel> GetAll()
        {
            List<SubjectModel> subjects = new List<SubjectModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"SELECT SubjectID, SubjectName FROM Subjects;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SubjectModel subject = new SubjectModel()
                        {
                            Id = (Guid)reader[0],
                            Name = (string)reader[1],
                        };
                        subjects.Add(subject);
                    }
                }
            }
            return subjects;
        }

        public void Delete(Guid subjectId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"DELETE FROM Grades WHERE SubjectID = @subjectId;
                      DELETE FROM Subjects WHERE SubjectID = @subjectId;";
                command.Parameters.Add("@subjectId", SqlDbType.UniqueIdentifier).Value = subjectId;
                command.ExecuteNonQuery();
            }
        }

        public void Add(string subjectName)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"INSERT INTO Subjects VALUES (NEWID(), @subjectName)";
                command.Parameters.Add("@subjectName", SqlDbType.NVarChar).Value = subjectName;
                command.ExecuteNonQuery();
            }
        }
    }
}
