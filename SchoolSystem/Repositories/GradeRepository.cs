﻿using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SchoolSystem.Repositories
{
    public class GradeRepository: RepositoryBase, IGradeRepository
    {
        public List<GradeModel> GetStudentGradesByStudentId(Guid studentId) {
            List<GradeModel> grades = new List<GradeModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"SELECT g.GradeID AS Id,
                             s.SubjectName,
                             g.Grade1,
                             g.Grade2
                     FROM Grades g
                     INNER JOIN Subjects s ON g.SubjectID = s.SubjectID
                     WHERE g.StudentID = @studentId;";
                command.Parameters.Add("@studentId", SqlDbType.UniqueIdentifier).Value = studentId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GradeModel grade = new GradeModel()
                        {
                            Id = (Guid)reader[0],
                            SubjectName = (string)reader[1],
                            Grade1 = (int)reader[2],
                            Grade2 = reader[3] == DBNull.Value ? null : (int)reader[3]
                        };
                        grades.Add(grade);
                    }
                }
            }
            return grades;
        }

        public void DeleteGrade(Guid gradeId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"DELETE FROM Grades WHERE GradeID = @gradeId;";
                command.Parameters.Add("@gradeId", SqlDbType.UniqueIdentifier).Value = gradeId;
                command.ExecuteNonQuery();
            }
        }

        public void AddGrade(Guid studentId, string subjectName, int grade1, int? grade2)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    @"DELETE FROM Grades
                      WHERE StudentID = @studentId
                      AND SubjectID = (SELECT SubjectID FROM Subjects WHERE SubjectName = @subjectName)

                      INSERT INTO Grades VALUES (NEWID(), (SELECT SubjectID FROM Subjects WHERE SubjectName = @subjectName), @studentId, @grade1, @grade2)";
                command.Parameters.Add("@studentId", SqlDbType.UniqueIdentifier).Value = studentId;
                command.Parameters.Add("@subjectName", SqlDbType.NVarChar).Value = subjectName;
                command.Parameters.Add("@grade1", SqlDbType.Int).Value = grade1;

                if (grade2.HasValue)
                {
                    command.Parameters.Add("@grade2", SqlDbType.Int).Value = grade2.Value;
                }
                else
                {
                    command.Parameters.Add("@grade2", SqlDbType.Int).Value = DBNull.Value;
                }

                command.ExecuteNonQuery();
            }
        }
    }
}
