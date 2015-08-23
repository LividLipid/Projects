using System;
using System.Collections.Generic;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class UserInterfaceConsoleMenu
    {
        public void ShowMenu(Menu menu)
        {
            var entries = menu.GetChildrenTitles();
            var entriesCount = entries.Count;

            var returnText = menu.IsRoot() ? "Quit" : "Return";
            //ReturnSelection = entriesCount + 1;

            //EntriesWithDefaults = entries;

            //Selection = FirstEntry;
            //Choice = 0;
            //ChoiceIsMade = false;

            //MenuTitle = menu.Title;
            //BuildMenuText();
        }
    }
}