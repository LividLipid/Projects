using System;
using Commands;
using Menu;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleScreenMenuAddNew : ConsoleScreenMenu
    {
        protected UIDataNewItem Data;

        public ConsoleScreenMenuAddNew(Handler handler, UIDataNewItem data) : base(handler, data)
        {
            Data = data;
            MenuHasItems = data.Names.Count >= 1;

            BuildMenu(data);
        }

        protected override void BuildMenuEntriesSection()
        {
            {
                var types = Data.CreatableTypes;
                var names = Data.Names;
                for (var i = 0; i < Data.Names.Count; i++)
                {
                    AddEntryLine(new CommandNewItemAdd(ItemHandler, types[i]), names[i]);
                }
                AddBlankLine();
            }
        }

        protected override void BuildMenuDefaultSection()
        {
            AddDefaultLine(new CommandReturn(ItemHandler));

            AddBlankLine();
            AddDefaultLine(new CommandQuit(ItemHandler));
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

        public void PrintNewItemMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Title);
            Console.WriteLine();
        }
    }
}