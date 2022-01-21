using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class DisenrollStudent
    {
        public int Id { get; set; }

        public Student Student { get; set; }

        public string Comment { get; set; }
    }
}
