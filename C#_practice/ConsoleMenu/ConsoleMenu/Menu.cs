using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleMenu
{
    [Serializable]
    public class Menu : IMenuItem
    {
        public string Title { get; set; }
        private List<IMenuItem> MenuItem;

        public Menu() { }
        public Menu(string title)
        {
            Title = title;
        }

        public void Select()
        {
            throw new NotImplementedException();
        }
    }
}