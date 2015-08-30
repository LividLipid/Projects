using System;
using System.Collections.Generic;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleScreenMenuMain : ConsoleScreenMenu
    {

        public ConsoleScreenMenuMain(UIData data, ConsoleUserInterface ui) : base(data, ui)
        {
        }

        public ConsoleScreenMenuMain(UIData data, ConsoleUserInterface ui, int cursorPosition) : base(data, ui, cursorPosition)
        {
        }

        protected override void SetMenuEntries(UIData data)
        {
            var menudata = (UIDataMenu) data;
            DataEntries = menudata.ChildrenTitles;
            DefaultEntries = new List<string>()
            {
                Operations.Null,
                Operations.Return,
                Operations.New,
                Operations.Save,
                Operations.Null,
                Operations.Quit,
            };
        }

        protected override void AddDataEntry()
        {
            EntryOperations.Add(Operations.Select);
        }

        protected override void AddDeletableEntry()
        {
            DeletableEntries.Add(true);
        }

        protected override void WriteInstructions(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine("Select item with arrow keys and Enter.");
            Console.WriteLine("Press Escape or Backspace to return.");
            if (DataEntries.Count > 0)
                Console.WriteLine("Press Delete to delete item.");
            Console.WriteLine("Press Ctrl+Z to undo.");
            Console.WriteLine("Press Ctrl+Y to redo.");
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
                case ConsoleKey.Delete:
                    ProcessDeleteKey();
                    break;
                case ConsoleKey.Z:
                    if (cki.Modifiers == ConsoleModifiers.Control)
                        ProcessUndoKey();
                    break;
                case ConsoleKey.Y:
                    if (cki.Modifiers == ConsoleModifiers.Control)
                        ProcessRedoKey();
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