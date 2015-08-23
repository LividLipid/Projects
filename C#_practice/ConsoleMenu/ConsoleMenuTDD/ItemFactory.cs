using System;

namespace ConsoleMenuTDD
{
    public static class ItemFactory
    {
        public static Item Create(Type itemType, string title)
        {
            switch (itemType.Name)
            {
                case "Menu":
                    return new Menu(title);
                case "Leaf":
                    return new Leaf(title);
                case "ItemSentinel":
                    return new ItemSentinel(title);
                default:
                    throw new ArgumentException("Unknown item type.");
            }
        }
    }
}