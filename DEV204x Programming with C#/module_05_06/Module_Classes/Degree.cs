using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_Classes
{
    public class Degree
    {
        public string DegreeName { get; set; }
        public int CreditsRequired { get; set; }
        public Course[] DegreeCourses { get; set; }
    }
}
