using System;

namespace ConsoleMenu
{
    public class MenuUI
    {
        public void ShowMenu(MenuParent menu)
        {
            var firstEntry = 1;
            var backSelection = menu.MenuItemCount + firstEntry;
            var quitSelection = backSelection + 1;
            

            var menuEntries = menu.GetChildrenTitles();
            menuEntries.Add("Back");
            menuEntries.Add("Quit");

            var selection = firstEntry;
            bool choiceIsMade = false;
            do
            {
                Console.Clear();
                int i = 0;
                foreach (var entry in menuEntries)
                {
                    i++;
                    var line = "[" + i + "] " + menuEntries[i - firstEntry];
                    if (i == selection)
                    {
                        WriteSelectedLine(line);
                    }
                    else
                    {
                        WriteNormalLine(line);
                    }
                }

                ConsoleKeyInfo cki = Console.ReadKey(true);
                ConsoleKey keyPress = cki.Key;
                char keyChar = cki.KeyChar;

                bool isDigit = char.IsDigit(keyChar);
                bool isChoiceMade = false;

                int choice;
                if (isDigit)
                {
                    int numValue = (int)char.GetNumericValue(keyChar);
                    choiceIsMade = (numValue >= firstEntry) && (numValue <= quitSelection);
                    if (choiceIsMade)
                    {
                        choice = numValue;
                    }
                }
                else
                {
                    switch (keyPress)
                    {
                        case ConsoleKey.Enter:
                            choice = selection;
                            choiceIsMade = true;
                            break;
                        case ConsoleKey.Escape:
                            choice = backSelection;
                            choiceIsMade = true;
                            break;
                        case ConsoleKey.Backspace:
                            choice = backSelection;
                            choiceIsMade = true;
                            break;
                        case ConsoleKey.UpArrow:
                            selection--;
                            break;
                        case ConsoleKey.DownArrow:
                            selection++;
                            break;
                    }
                }

                if (selection < firstEntry)
                    selection = quitSelection;
                if (selection > quitSelection)
                    selection = firstEntry;
            } while (!choiceIsMade);


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
    }
}