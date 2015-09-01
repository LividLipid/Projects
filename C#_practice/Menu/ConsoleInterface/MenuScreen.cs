using System;
using System.Collections.Generic;
using Commands;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public abstract class MenuScreen : ConsoleScreen
    {
        protected MenuScreen(UIData data, CommandFactory cmdFactory) : base(data, cmdFactory)
        {
            var menuData = (UIDataMenu) data;
        }

        protected override void ArrangeEntries(UIData dataObject)
        {
            var dataTitles = GetDataTitles((UIDataMenu) dataObject);
            int i = 0;
            foreach (var title in dataTitles)
            {
                AddDataEntry(title, i);
                i++;
            }
                

            var defaultOperations = GetDefaultOperations();
            foreach (var o in defaultOperations)
                AddDefaultEntry(o);
        }

        protected abstract List<string> GetDataTitles(UIDataMenu dataObject);
        protected abstract List<string> GetDefaultOperations();
        protected abstract void AddDataEntry(string data, int dataIndex);

        protected void AddDefaultEntry(string operation)
        {
            string text = operation;
            string data = null;
            bool isDeletable = false;
            int dataIndex = -1;
            var newEntry = new MenuEntry(text, data, operation, isDeletable, dataIndex);
            Entries.Add(newEntry);
        }

        protected override void PrintMenuText()
        {
            WriteTitleSection(ConsoleColor.Red);
            if (CountDataEntries() == 0)
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
            for (var i = 0; i < Entries.Count; i++)
            {
                if (i == CursorPosition)
                    if (LineActivation[i] == true)
                        WriteHighlightedLine(Entries[i].Text);
                    else
                        WriteDisabledHighlightedLine(Entries[i].Text);
                else
                    if (LineActivation[i] == true)
                        WriteLine(Entries[i].Text);
                    else
                        WriteDisabledLine(Entries[i].Text);
            }
        }
    }
}