﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Models
{
    public class AddSubjectRequest
    {
        public string Name { get; set; }

        public int? TeacherId { get; set; }
    }
}
