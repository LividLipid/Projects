using System;
using System.Collections.Generic;
using Commands;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen(UIData data, CommandFactory cmdFactory) : base(data, cmdFactory)
        {
        }

        protected override List<string> GetDataTitles(UIDataMenu dataObject)
        {
            return dataObject.ChildrenTitles;
        }

        protected override List<string> GetDefaultOperations()
        {
            
            var defaultOperations = new List<string>()
            {
                Operations.Null,
                Operations.Return,
                Operations.New,
                Operations.Save,
                Operations.Null,
                Operations.Quit,
            };
            if (InputData.IsRoot)
                defaultOperations.Remove(Operations.Return);

            return defaultOperations;
        }

        protected override void AddDataEntry(string data, int dataIndex)
        {
            string text = data;
            string operation = Operations.Select;
            bool isDeletable = true;
            var newEntry = new MenuEntry(text, data, operation, isDeletable, dataIndex);
            Entries.Add(newEntry);
        }

        protected override void WriteInstructions(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine("Select item with arrow keys and Enter.");
            Console.WriteLine(InputData.IsRoot
                ? "Press Escape to quit."
                : "Press Escape to quit or Backspace to return.");
            if (CountDataEntries() > 0)
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
                    if (!InputData.IsRoot)
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