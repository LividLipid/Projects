using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MenuItems;
using UserInterfaceBoundary;

namespace MenuSystem
{
    public static class UIDataFactory
    {
        public static UIData CreateUIData(Item item)
        {
            var type = item.GetType();
            if (type == typeof (ItemLeaf))
                return CreateLeafData((ItemLeaf) item);
            if (type == typeof(ItemLeafRSS))
                return CreateLeafRSSData((ItemLeafRSS) item);
            if (type == typeof(ItemMenu))
                return CreateMenuData((ItemMenu) item);
            if (type == typeof(ItemSentinel))
                throw new Exception("A sentinel has no data.");
            throw new Exception("Unrecognized item type.");
        }

        private static UIDataLeaf CreateLeafData(ItemLeaf item)
        {
            var title = item.Title;

            return new UIDataLeaf(title);
        }

        private static UIDataLeaf CreateLeafRSSData(ItemLeafRSS item)
        {
            var title = item.Title;
            var address = item.Address;

            return new UIDataLeafRSS(title, address);
        }

        private static UIDataMenu CreateMenuData(ItemMenu item)
        {
            var title = item.Title;
            var childrenTitles = item.GetChildrenTitles();
            var creatableTypes = Item.GetCreatableItemTypes();
            var typeNames = creatableTypes.Select(Item.GetNameOfItemType).ToList();

            return new UIDataMenu(title, childrenTitles, creatableTypes, typeNames);
        }
    }
}