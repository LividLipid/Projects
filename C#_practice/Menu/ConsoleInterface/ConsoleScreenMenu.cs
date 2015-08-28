using System;
using System.Collections.Generic;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public abstract class ConsoleScreenMenu : ConsoleScreen
    {
        protected List<string> DataEntries;
        protected List<string> DefaultEntries;
        
        protected ConsoleScreenMenu(UIData data, ConsoleUserInterface ui) : base(data, ui)
        {
        }

        protected ConsoleScreenMenu(UIData data, ConsoleUserInterface ui, int cursorPosition) : base(data, ui, cursorPosition)
        {
        }

        protected abstract void ArrangeDataSection();
        protected abstract void ArrangeDefaultSection();
        protected abstract void WriteInstructions(ConsoleColor color);

        protected override void ArrangeEntriesAndOperations()
        {
            ArrangeDataSection();
            ArrangeDefaultSection();
        }

        protected override void PrintMenuText()
        {
            WriteTitleSection(ConsoleColor.Red);
            if (DataEntries.Count == 0)
                WriteEmptyEntryMessage(ConsoleColor.Yellow);
            WriteEntrySection();
            WriteInstructions(ConsoleColor.Red);
        }

        protected void WriteTitleSection(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.ResetColor();
        }

        protected void WriteEmptyEntryMessage(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("This menu contains no items.");
            Console.WriteLine();
            Console.ResetColor();
        }

        protected void WriteEntrySection()
        {
            for (int i = 0; i < Entries.Count; i++)
            {
                if (i == CursorPosition)
                    WriteHighlightedLine(Entries[i]);
                else
                    WriteLine(Entries[i]);
            }
        }
    }
}