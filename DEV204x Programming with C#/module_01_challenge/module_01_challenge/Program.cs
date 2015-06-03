using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_01_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstName = GetLine("First name");
            string lastName = GetLine("Last name");
            int birthYear = int.Parse(GetLine("Year of birth"));
            int birthMonth = int.Parse(GetLine("Month of birth"));
            int birthDayOfMonth = int.Parse(GetLine("Day of month of birth"));
            DateTime birthdate = new DateTime(birthYear, birthMonth, birthDayOfMonth);
            string adressLine1 = GetLine("Address line 1");
            string adressLine2 = GetLine("Address line 2");
            string city = GetLine("City");
            string stateProvince = GetLine("State/Provine");
            int zipPostal = int.Parse(GetLine("Zip/Postal"));
            string country = GetLine("Country");

            Console.WriteLine("First name: {0}", firstName);
            Console.WriteLine("Last name: {0}", lastName);
            Console.WriteLine("Birthdate: {0}", birthdate);
            Console.WriteLine("Address line 1: {0}", adressLine1);
            Console.WriteLine("Address line 2: {0}", adressLine2);
            Console.WriteLine("City: {0}", city);
            Console.WriteLine("State/Province: {0}", stateProvince);
            Console.WriteLine("Zip/Postal: {0}", zipPostal);
            Console.WriteLine("Country: {0}", country);
            Console.WriteLine();
        }

        static string GetLine(string request)
        {
            Console.WriteLine("Enter " + request);
            Console.WriteLine();

            Console.Write("   ");
            string line = Console.ReadLine();
            if (line != null)
            {
                Console.WriteLine("      " + line);
            }
            Console.WriteLine();

            return line;
        }
    }
}
