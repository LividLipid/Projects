using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class MenuUI
    {
        private const int FirstEntry = 1;
        private readonly int ReturnSelection;
        private const int ReturnChoice = -1;
        private List<string> MenuText;
        private readonly List<string> EntriesWithDefaults;
        private int Selection;
        private int Choice;
        private bool ChoiceIsMade;
 
        public MenuUI(MenuParent menu)
        {
            var entries = menu.GetChildrenTitles();
            var entriesCount = entries.Count;

            entries.Add("Back");
            ReturnSelection = entriesCount + 1;

            EntriesWithDefaults = entries;

            Selection = FirstEntry;
            Choice = 0;
            ChoiceIsMade = false;

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

        public int ShowMenu()
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

            return Choice;
        }

        private void PrintMenuText()
        {
            Console.Clear();
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
                    Choice = ReturnChoice;
                    ChoiceIsMade = true;
                    break;
                case ConsoleKey.Backspace:
                    Choice = ReturnChoice;
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