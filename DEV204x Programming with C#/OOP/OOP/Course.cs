using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Course
    {
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DurationInWeeks { get; set; }
        public Teacher[] CourseTeachers { get; set; }
        public Student[] CourseStudents { get; set; }
    }
}
