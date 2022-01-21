using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class AddAndUpdateStudentRequest
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
