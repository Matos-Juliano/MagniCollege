using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class CourseDetailsView
    {
        public CourseDTO Course { get; set; }

        public List<StudentDTO> Students { get; set; }

        public List<SubjectDTO> Subjects { get; set; }

        public double AverageGrade { get; set; }

        public int TeachersTotal { get; set; }
    }
}
