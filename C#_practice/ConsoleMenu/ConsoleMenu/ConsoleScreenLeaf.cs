using System;

namespace ConsoleMenu
{
    public class ConsoleScreenLeaf : ConsoleScreen
    {

        public ConsoleScreenLeaf(Handler handler, UIData data) : base(handler, data)
        {

        }

        public override void Display()
        {
            PrintLeafText();
            ReadKey();
            IssureReturnCommand();
        }

        public virtual void PrintLeafText()
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

        public virtual void ReadKey()
        {
            Console.ReadKey(true);
        }

        private void IssureReturnCommand()
        {
            ChosenCommand = new CommandReturn(ItemHandler);
            ChosenCommand.Execute();
        }

    }
}