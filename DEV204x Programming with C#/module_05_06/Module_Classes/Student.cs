using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_Classes
{
    public class Student : Person
    {
        public static int NrOfStudentsEnrolled = 0;

        public Student()
        {
            NrOfStudentsEnrolled++;
        }

        public void TakeTest()
        {
            
        }
    }
}
