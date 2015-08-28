using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuItems
{
    [Serializable]
    public abstract class Item
    {
        public Item Parent;
        public string Title { get; } 
        public int ChildrenCount { get; set; }
        public int SiblingNr { get; set; }

        protected Item(string title)
        {
            Title = title;
            ChildrenCount = 0;
        }

        public abstract void AddChild(Item child);
        public abstract Item GetChild(int i);
        public abstract void RemoveChild(int i);
        public abstract void RemoveChild(Item item);
        public abstract bool IsSentinel();
        public abstract bool IsLeaf();
        public abstract bool IsMenu();
        public abstract List<ItemLeaf> GetSubTreeLeaves();
        public abstract string GetItemTypeName();

        public bool IsRoot()
        {
            return Parent.IsSentinel();
        }

        public Item GetRoot()
        {
            if (IsRoot())
                return this;
            var i = new IteratorParentWalk(this);
            return (ItemMenu)i.GetFinal();
        }

        public bool HasInTree(Item item)
        {
            var root = GetRoot();
            var isInTree = root.HasInSubTree(item);

            return isInTree;
        }

        public bool HasInSubTree(Item item)
        {
            var i = new IteratorLevelOrderWalk(this);
            var foundItem = i.SearchForItem(item);
            var isInSubTree = !foundItem.IsSentinel();

            return isInSubTree;
        }

        public static List<Type> GetCreatableItemTypes()
        {
            var subTypes = typeof(Item).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Item))).GetEnumerator();
            var list = new List<Type>();
            while (subTypes.MoveNext())
            {
                list.Add(subTypes.Current);
            }
            list.Remove(typeof (ItemSentinel));
            return list;
        }
    }
}