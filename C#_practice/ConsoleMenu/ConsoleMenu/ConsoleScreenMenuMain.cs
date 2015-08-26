using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class ConsoleScreenMenuMain : ConsoleScreenMenu
    {
        protected UIDataMenu Data;

        public ConsoleScreenMenuMain(Handler handler, UIDataMenu data) : base(handler, data)
        {
            Data = data;
            MenuHasItems = data.ChildrenTitles.Count >= 1;

            BuildMenu(data);
        }

        protected override void BuildMenuEntriesSection()
        {
            {
                var titles = Data.ChildrenTitles;
                for (var i = 0; i < titles.Count; i++)
                {
                    AddEntryLine(new CommandSelect(ItemHandler, i), titles[i]);
                }
                AddBlankLine();
            }
        }

        protected override sealed void BuildMenuDefaultSection()
        {
            AddDefaultLine(new CommandReturn(ItemHandler));
            AddDefaultLine(new CommandRequestNewItem(ItemHandler));
            AddDefaultLine(new CommandSave(ItemHandler));

            AddBlankLine();
            AddDefaultLine(new CommandQuit(ItemHandler));
        }

        protected override void WriteInstructions(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine("Select item with arrow keys and Enter.");
            Console.WriteLine("Press Escape or Backspace to return.");
            if (MenuHasItems)
                Console.WriteLine("Press Delete to delete item.");
            Console.WriteLine();
            Console.ResetColor();
        }

        protected override void ProcessNonDigitInput(ConsoleKey keyPress)
        {
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
                case ConsoleKey.UpArrow:
                    ProcessUpArrowKey();
                    break;
                case ConsoleKey.DownArrow:
                    ProcessDownArrowKey();
                    break;
            }
        }

        protected override string RequestTextInput()
        {
            return "";
        }
    }
}