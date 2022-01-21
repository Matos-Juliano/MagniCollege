using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubjectDTO> Subjects { get; set; }
    }
}
