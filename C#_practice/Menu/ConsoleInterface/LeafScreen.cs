using System;
using Commands;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class LeafScreen : ConsoleScreen
    {
        public LeafScreen(UIData data, CommandFactory cmdFactory) : base(data, cmdFactory)
        {
        }

        protected override void ArrangeEntries(UIData data)
        {
            throw new NotImplementedException();
        }

        protected override void PrintMenuText()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Title);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Empty leaf.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press any key to return.");
            Console.WriteLine();
            Console.ResetColor();
        }

        protected override void ProcessNonDigitInput(ConsoleKeyInfo cki)
        {
            throw new NotImplementedException();
        }

        
    }
}