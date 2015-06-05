using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Module_Classes;

namespace Module_05
{
    class Program
    {
        static void Main(string[] args)
        {
            const int nrOfStudents = 3;
            const int nrOfTeachers = 1;

            var students = new Student[nrOfStudents];
            for (int i = 0; i < nrOfStudents; i++)
            {
                students[i] = new Student();
            }
            
            var teachers = new Teacher[nrOfTeachers];
            for (int i = 0; i < nrOfTeachers; i++)
            {
                teachers[i] = new Teacher();
            }

            var programmingWithCSharp = new Course 
            {
                CourseName = "Programming with C#",
                CourseStudents = students, 
                CourseTeachers = teachers
            };

            var courses = new Course[1] {programmingWithCSharp};
            var bachelor = new Degree
            {
                DegreeName = "Bachelor",
                DegreeCourses = courses
            };

            var degrees = new Degree[1] {bachelor};
            var informationTechnology = new UProgram
            {
                ProgramName = "Information Technology",
                ProgramDegrees = degrees
            };


            OutputProgramInfo(informationTechnology);
        }

        static void OutputProgramInfo(UProgram prog)
        {
            foreach (Degree t in prog.ProgramDegrees)
            {
                Console.WriteLine("The " + prog.ProgramName + " contains the degree " + t.DegreeName);

                foreach (Course u in t.DegreeCourses)
                {
                    Console.WriteLine("    The " + t.DegreeName + " degree contains the course " + u.CourseName);
                    Console.WriteLine("        The course " + u.CourseName + " has " + u.CourseStudents.Length + " student(s) enrolled");
                    Console.WriteLine();
                }
            }
        }
    }
}
