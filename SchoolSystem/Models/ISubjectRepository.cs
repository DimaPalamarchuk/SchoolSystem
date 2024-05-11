using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Models
{
    public interface ISubjectRepository
    {
        public List<SubjectModel> GetAll();
        void Delete(Guid subjectId);
        void Add(string subjectName);
    }
}
