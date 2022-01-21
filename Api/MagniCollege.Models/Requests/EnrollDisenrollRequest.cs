using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class EnrollDisenrollRequest
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string Comment { get; set; }
    }
}
