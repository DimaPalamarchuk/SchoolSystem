using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Models
{
    public class GradeModel
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public int Grade1 { get; set; }
        public int? Grade2 { get; set; }
    }
}
