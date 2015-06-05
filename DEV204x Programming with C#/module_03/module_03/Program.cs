using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_03
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var validInput = false;
            const string pattern = "dd-MM-yyyy";

            do
            {
                try
                {
                    DateTime parsedDate;
                    Console.WriteLine("Please input date of birth (dd-mm-yyyy): ");
                    string input = Console.ReadLine();
                    validInput = DateTime.TryParseExact(input, pattern, null,
                        DateTimeStyles.None, out parsedDate);
                    if (validInput)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Good job!");
                        Console.WriteLine();
                    }
                    else
                    {
                        var exc = new Exception("Invalid date format");
                        throw exc;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }
            } while (!validInput);
        }
    }
}