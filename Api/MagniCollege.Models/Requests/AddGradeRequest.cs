using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class AddGradeRequest
    {
        public int StudentId { get; set; }

        public double Value { get; set; }
    }
}
