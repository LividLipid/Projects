using System;

namespace ConsoleMenu
{
    public class UserInterfaceConsoleLeaf
    {
        private string _title ;
        private Handler _handler;
        private Command _chosenCommand;

        public UserInterfaceConsoleLeaf(Handler handler, DataLeaf data)
        {
            _title = data.LeafTitle;
            _handler = handler;
        }

        public virtual void DisplayLeaf()
        {
            PrintLeafText();
            ReadKey();
            IssureReturnCommand();
            _chosenCommand.AddToCommandQueue();
        }

        public virtual void PrintLeafText()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine(_title);
            Console.WriteLine();
            Console.WriteLine("Empty leaf.");
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
            _chosenCommand = new CommandReturn(_handler);
        }
    }
}