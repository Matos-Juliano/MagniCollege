using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class NotCreatedException : Exception
    {
        public NotCreatedException()
        {

        }

        public NotCreatedException(string message) : base(message)
        {

        }
    }
}
