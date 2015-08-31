using System;
using System.Collections.Generic;
using Commands;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class CreateNewMenuScreen : MenuScreen
    {
        public CreateNewMenuScreen(UIData data, CommandFactory cmdFactory) : base(data, cmdFactory)
        {
        }

        protected override List<string> GetDataTitles(UIDataMenu dataObject)
        {
            return dataObject.CreatableTypeNames;
        }

        protected override List<string> GetDefaultOperations()
        {
            var defaultEntries = new List<string>()
            {
                Operations.Null,
                Operations.Return,
                Operations.Null,
                Operations.Quit,
            };

            return defaultEntries;
        }

        protected override void AddDataEntry(string data, int dataIndex)
        {
            string text = data;
            string operation = Operations.Create;
            bool isDeletable = false;
            var newEntry = new MenuEntry(text, data, operation, isDeletable, dataIndex);
            Entries.Add(newEntry);
        }

        protected override void WriteInstructions(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine("Select item with arrow keys and Enter.");
            Console.WriteLine("Press Escape or Backspace to return.");
            Console.WriteLine();
            Console.ResetColor();
        }

        protected override void ProcessNonDigitInput(ConsoleKeyInfo cki)
        {
            var keyPress = cki.Key;
            switch (keyPress)
            {
                case ConsoleKey.Enter:
                    ProcessEnterKey();
                    break;
                case ConsoleKey.Escape:
                    ProcessEscapeKey();
                    break;
                case ConsoleKey.Backspace:
                    ProcessBackspaceKey();
                    break;
                case ConsoleKey.UpArrow:
                    ProcessUpArrowKey();
                    break;
                case ConsoleKey.DownArrow:
                    ProcessDownArrowKey();
                    break;
            }
        }
    }
}