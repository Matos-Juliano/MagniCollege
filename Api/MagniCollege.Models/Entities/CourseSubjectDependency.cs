using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class CourseSubjectDependency
    {
        public int Id { get; set; }

        public Course Course { get; set; }

        public Subject Subject { get; set; }
    }
}
