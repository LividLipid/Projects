using System;

namespace ConsoleMenu
{
    [Serializable]
    public abstract class MenuNode
    {
        public string Title;
        public int MenuItemCount = 0;
        public MenuNode Parent;

        public MenuNode(string title, MenuNode parent)
        {
            Title = title;
            Parent = parent ?? this;
        }
    }
}