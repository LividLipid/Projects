using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class ConsoleScreenMenuMain : ConsoleScreenMenu
    {
        public ConsoleScreenMenuMain(Handler handler, UIDataMenu data) : base(handler, data)
        {
            var entries = data.ChildrenTitles;
            MenuHasItems = entries.Count >= 1;

            BuildMenu(entries);
        }

        protected override void BuildMenuEntriesSection(List<string> entries)
        {
            {
                for (var i = 0; i < entries.Count; i++)
                {
                    AddSelectLine(new CommandSelect(ItemHandler, i), entries[i]);
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