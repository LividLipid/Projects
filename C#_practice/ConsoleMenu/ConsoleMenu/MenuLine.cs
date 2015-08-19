using System;

namespace ConsoleMenu
{
    [Serializable]
    public class MenuLine : IMenuItem
    {
        public string Title { get; set; }

        public MenuLine() { }

        public MenuLine(string title)
        {
            Title = title;
        }
        public void Select()
        {
            throw new System.NotImplementedException();
        }
    }
}