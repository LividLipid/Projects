using System;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleScreenLeaf : ConsoleScreen
    {

        public ConsoleScreenLeaf(UIData data, ConsoleUserInterface ui) : base(data, ui)
        {
        }

        public ConsoleScreenLeaf(UIData data, ConsoleUserInterface ui, int cursorPosition) : base(data, ui, cursorPosition)
        {
        }

        protected override void ArrangeEntriesAndOperations(UIData data)
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