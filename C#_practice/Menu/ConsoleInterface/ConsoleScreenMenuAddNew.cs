using System;
using System.Collections.Generic;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleScreenMenuAddNew : ConsoleScreenMenu
    {

        public ConsoleScreenMenuAddNew(UIData data, ConsoleUserInterface ui) : base(data, ui)
        {
        }

        public ConsoleScreenMenuAddNew(UIData data, ConsoleUserInterface ui, int cursorPosition) : base(data, ui, cursorPosition)
        {
        }

        protected override void SetMenuEntries(UIData data)
        {

            var menudata = (UIDataNewTypes) data;
            DataEntries = menudata.Names;
            DefaultEntries = new List<string>()
            {
                Operations.Null,
                Operations.Return,
                Operations.Null,
                Operations.Quit,
            };
        }

        protected override void AddDataEntry()
        {
            EntryOperations.Add(Operations.Create);
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