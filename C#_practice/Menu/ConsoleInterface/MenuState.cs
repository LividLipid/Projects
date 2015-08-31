using System;
using System.Collections.Generic;

namespace ConsoleInterface
{
    [Serializable]
    public class MenuState
    {
        public List<MenuEntry> Entries;

        public MenuState(List<MenuEntry> entries)
        {
            Entries = entries;
        }
    }
}