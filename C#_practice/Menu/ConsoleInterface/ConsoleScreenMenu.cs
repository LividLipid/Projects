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

        protected override void ArrangeEntriesAndOperations(UIData data)
        {
            SetMenuEntries(data);
            ArrangeDataSection();
            ArrangeDefaultSection();
        }

        protected abstract void SetMenuEntries(UIData data);

        protected void ArrangeDataSection()
        {
            var i = 0;
            foreach (var t in DataEntries)
            {
                Entries.Add(t);
                AddDataEntry();
                DeletableEntries.Add(false);
                EntryDataIndices.Add(i);
                i++;
            }
        }

        protected abstract void AddDataEntry();

        protected void ArrangeDefaultSection()
        {
            if (DefaultEntries.Count == 0) return;
            foreach (var t in DefaultEntries)
            {
                Entries.Add(t);
                EntryOperations.Add(t);
                DeletableEntries.Add(false);
                EntryDataIndices.Add(-1);
            }
        }

        protected override void PrintMenuText()
        {
            WriteTitleSection(ConsoleColor.Red);
            if (DataEntries.Count == 0)
                WriteEmptyEntryMessage(ConsoleColor.Yellow);
            WriteEntrySection();
            WriteInstructions(ConsoleColor.Red);
        }

        protected abstract void WriteInstructions(ConsoleColor color);

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