using System;

namespace ConsoleMenu
{
    public class ConsoleScreenLeaf : ConsoleScreen
    {

        public ConsoleScreenLeaf(Handler handler, Data data) : base(handler, data)
        {

        }

        public override void Display()
        {
            PrintLeafText();
            ReadKey();
            IssureReturnCommand();
            ChosenCommand.AddToCommandQueue();
        }

        public virtual void PrintLeafText()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.WriteLine("Empty leaf.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return.");
            Console.WriteLine();
            Console.ResetColor();
        }

        public virtual void ReadKey()
        {
            Console.ReadKey(true);
        }

        private void IssureReturnCommand()
        {
            ChosenCommand = new CommandReturn(Handler);
        }

    }
}