using System;
using System.Collections.Generic;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleScreenMenuAddNew : ConsoleScreenMenu
    {

        public ConsoleScreenMenuAddNew(UIDataNewItem data, ConsoleUserInterface ui) : base(data, ui)
        {
            SetupAddNewMenu(data);
        }

        public ConsoleScreenMenuAddNew(UIDataNewItem data, ConsoleUserInterface ui, int cursorPosition) : base(data, ui, cursorPosition)
        {
            SetupAddNewMenu(data);
        }

        private void SetupAddNewMenu(UIDataNewItem data)
        {
            DataEntries = data.Names;
            DefaultEntries = new List<string>()
            {
                OperationBlank,
                OperationReturn,
                OperationBlank,
                OperationQuit
            };
        }

        protected override void ArrangeDataSection()
        {
            foreach (var t in DataEntries)
            {
                Entries.Add(t);
                Operations.Add(OperationAddNew);
                DeletableEntries.Add(false);
            }
        }

        protected override void ArrangeDefaultSection()
        {
            if (DefaultEntries.Count == 0) return;
            foreach (var t in DefaultEntries)
            {
                Entries.Add(t);
                Operations.Add(t);
                DeletableEntries.Add(false);
            }
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