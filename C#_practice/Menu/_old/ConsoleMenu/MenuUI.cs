using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class MenuUI
    {
        public static int ReturnCode = -1;
        private const int FirstEntry = 1;
        private readonly int ReturnSelection;
        private List<string> MenuText;
        private readonly List<string> EntriesWithDefaults;
        private int Selection;
        private int Choice;
        private bool ChoiceIsMade;
        private string MenuTitle;
 
        public MenuUI(MenuParent menu)
        {
            var entries = menu.GetChildrenTitles();
            var entriesCount = entries.Count;

            entries.Add(menu.IsRoot ? "Quit" : "Return");
            ReturnSelection = entriesCount + 1;

            EntriesWithDefaults = entries;

            Selection = FirstEntry;
            Choice = 0;
            ChoiceIsMade = false;

            MenuTitle = menu.Title;
            BuildMenuText();
        }

        private void BuildMenuText()
        {
            List<string> menuText = new List<string>();
            for (int i = 0; i < EntriesWithDefaults.Count; i++)
            {
                int entryNr = i + FirstEntry;
                menuText.Add("[" + entryNr + "] " + EntriesWithDefaults[i]);
            }
            MenuText = menuText;
        }

        public int ShowUI()
        {
            do
            {
                PrintMenuText();

                ConsoleKeyInfo cki = Console.ReadKey(true);
                bool keyIsDigit = char.IsDigit(cki.KeyChar);
                if (keyIsDigit)
                    ProcessDigitInput(cki.KeyChar);
                else
                    ProcessNonDigitInput(cki.Key);

                LoopSelection();
            } while (!ChoiceIsMade);

            if (Choice == ReturnSelection)
                Choice = ReturnCode;
            return Choice;
        }

        private void PrintMenuText()
        {
            Console.Clear();
            Console.WriteLine(MenuTitle);
            Console.WriteLine();

            for (int i = 0; i < MenuText.Count; i++)
            {
                if (i+FirstEntry == Selection)
                {
                    WriteSelectedLine(MenuText[i]);
                }
                else
                {
                    WriteNormalLine(MenuText[i]);
                }
            }
        }

        private void WriteNormalLine(string line)
        {
            Console.WriteLine(line);
        }

        private void WriteSelectedLine(string line)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        private void ProcessDigitInput(char keyChar)
        {
            int numValue = (int)char.GetNumericValue(keyChar);
            bool isWithinBounds = (numValue >= FirstEntry) && (numValue <= ReturnSelection);
            if (isWithinBounds)
            {
                Choice = numValue;
            }
        }

        private void ProcessNonDigitInput(ConsoleKey keyPress)
        {
            switch (keyPress)
            {
                case ConsoleKey.Enter:
                    Choice = Selection;
                    ChoiceIsMade = true;
                    break;
                case ConsoleKey.Escape:
                    Choice = ReturnCode;
                    ChoiceIsMade = true;
                    break;
                case ConsoleKey.Backspace:
                    Choice = ReturnCode;
                    ChoiceIsMade = true;
                    break;
                case ConsoleKey.UpArrow:
                    Selection--;
                    break;
                case ConsoleKey.DownArrow:
                    Selection++;
                    break;
                }
        }

        private void LoopSelection()
        {
            if (Selection < FirstEntry)
                Selection = ReturnSelection;
            if (Selection > ReturnSelection)
                Selection = FirstEntry;
        }
    }
}