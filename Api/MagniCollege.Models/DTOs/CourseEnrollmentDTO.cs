using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class CourseEnrollmentDTO
    {
        public int Id { get; set; }

        public Course Course { get; set; }

        public Student Student { get; set; }

        public bool IsEnrolled { get; set; }
    }
}
