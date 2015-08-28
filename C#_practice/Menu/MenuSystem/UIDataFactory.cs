using System;
using MenuItems;
using UserInterfaceBoundary;

namespace MenuSystem
{
    public class UIDataFactory
    {
        public static UIData CreateUIData(Item item)
        {
            var itemType = item.GetType();
            if (itemType == typeof (ItemLeaf))
                return CreateLeafData((ItemLeaf) item);
            else if (itemType == typeof(ItemLeafRSS))
                return CreateLeafRSSData((ItemLeafRSS) item);
            else if (itemType == typeof(ItemMenu))
                return CreateMenuData((ItemMenu) item);
            else if (itemType == typeof(ItemSentinel))
                throw new Exception("A sentinel has no data.");
            else
                throw new Exception("Unrecognized item type.");
        }

        private static UIDataLeaf CreateLeafData(ItemLeaf item)
        {
            return new UIDataLeaf(item.Title);
        }

        private static UIDataLeaf CreateLeafRSSData(ItemLeafRSS item)
        {
            return new UIDataLeafRSS(item.Title, item.Address);
        }

        private static UIDataMenu CreateMenuData(ItemMenu item)
        {
            return new UIDataMenu(item.Title, item.GetChildrenTitles());
        }
    }
}