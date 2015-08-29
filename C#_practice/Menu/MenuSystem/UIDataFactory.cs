using System;
using System.Collections.Generic;
using System.Linq;
using MenuItems;
using UserInterfaceBoundary;

namespace MenuSystem
{
    public static class UIDataFactory
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

        public static UIDataNewTypes CreateNewTypesData()
        {
            var title = "Create new item Menu";
            var creatableTypes = Item.GetCreatableItemTypes();
            var typeNames = creatableTypes.Select(Item.GetNameOfItemType).ToList();
            
            return new UIDataNewTypes(title, creatableTypes, typeNames);
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