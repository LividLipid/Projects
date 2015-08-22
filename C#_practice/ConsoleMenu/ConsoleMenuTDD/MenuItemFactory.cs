using System;

namespace ConsoleMenuTDD
{
    public static class MenuItemFactory
    {
        public static MenuItem Create(Type itemType, string title)
        {
            switch (itemType.Name)
            {
                case "Menu":
                    return new Menu(title);
                case "Leaf":
                    return new Leaf(title);
                case "Sentinel":
                    return new Sentinel(title);
                default:
                    throw new ArgumentException("Unknown item type.");
            }
        }
    }
}