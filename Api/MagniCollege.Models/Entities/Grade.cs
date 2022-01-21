using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public Student Student { get; set; }

        public Subject Subject { get; set; }

        public double Value { get; set; }
    }
}
