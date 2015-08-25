using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class ConsoleScreenMenuAddNew : ConsoleScreenMenu
    {
        private List<Type> _creatableTypes;
         
        public ConsoleScreenMenuAddNew(Handler handler, UIDataNewItem data) : base(handler, data)
        {
            _creatableTypes = data.CreatableTypes;
            var entries = data.Names;
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("Please type the title of the new item and press enter.");
            Console.WriteLine("Press Escape to cancel");
            Console.WriteLine();
            Console.ResetColor();
            return Console.ReadLine();
        }

        public void PrintNewItemMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Title);
            Console.WriteLine();
        }
    }
}