using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_Classes;

namespace Module_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            Student student = new Student();
            Student student2 = new Student();

            person = student;
        }
    }
}
