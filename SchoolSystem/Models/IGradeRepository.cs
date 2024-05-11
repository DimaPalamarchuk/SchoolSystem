using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Models
{
    public interface IGradeRepository
    {
        List<GradeModel> GetStudentGradesByStudentId(Guid studentId);
        void DeleteGrade(Guid gradeId);
        void AddGrade(Guid studentId, string subjectName, int grade1, int? grade2);
    }
}
