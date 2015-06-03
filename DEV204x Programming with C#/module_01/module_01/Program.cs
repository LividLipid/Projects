using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_01
{
    class Program
    {
        static void Main(string[] args)
        {
            const string firstName = "Ask";
            const string lastName = "Schou";
            DateTime birthdate = new DateTime(1985,3,23);
            const string adressLine1 = "Borgergade 18, 3. tv";
            const string adressLine2 = "";
            const string city = "Aalborg";
            const string stateProvince = "";
            const int zipPostal = 9000;
            const string country = "Denmark";

            Console.WriteLine("First name: {0}", firstName);
            Console.WriteLine("Last name: {0}", lastName);
            Console.WriteLine("Birthdate: {0}", birthdate);
            Console.WriteLine("Adress line 1: {0}", adressLine1);
            Console.WriteLine("Adress line 2: {0}", adressLine2);
            Console.WriteLine("City: {0}", city);
            Console.WriteLine("State/Province: {0}", stateProvince);
            Console.WriteLine("Zip/Postal: {0}", zipPostal);
            Console.WriteLine("Country: {0}", country);
        }
    }
}
