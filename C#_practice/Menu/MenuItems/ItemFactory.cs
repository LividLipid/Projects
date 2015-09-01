using System;

namespace MenuItems
{
    public static class ItemFactory
    {
        public static Item Create(Type itemType, string title, string textData)
        {
            switch (itemType.Name)
            {
                case "ItemMenu":
                    return new ItemMenu(title);
                case "ItemLeaf":
                    return new ItemLeaf(title);
                case "ItemLeafRSS":
                    return new ItemLeafRSS(title, textData);
                case "ItemSentinel":
                    return new ItemSentinel(title);
                default:
                    throw new ArgumentException("Unknown item type.");
            }
        }
    }
}