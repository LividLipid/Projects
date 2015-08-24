using System;
using System.Collections.Generic;

namespace ConsoleMenuTDD
{
    [Serializable]
    public abstract class Item
    {
        // The title uniquely identifies an item,
        // and the same item cannot exist twice in the same tree.
        public Handler TreeHandler;
        public Item Parent;
        public string Title { get; } 
        public int ChildrenCount { get; set; }

        protected Item(string title)
        {
            Title = title;
            ChildrenCount = 0;
        }

        public abstract void AddChild(Item child);
        public abstract Item GetChild(int i);
        public abstract void RemoveChild(int i);
        public abstract bool IsSentinel();
        public abstract bool IsLeaf();
        public abstract List<Leaf> GetSubTreeLeaves();

        public bool IsRoot()
        {
            return Parent.IsSentinel();
        }

        public Handler GetHandler()
        {
            return IsRoot() ? TreeHandler : GetRoot().GetHandler();
        }

        public Item GetRoot()
        {
            if (IsRoot())
                return this;
            var i = new IteratorParentWalk(this);
            return (Menu)i.GetFinal();
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
            var foundItem = i.SearchForTitle(item.Title);
            var isInSubTree = !foundItem.IsSentinel();

            return isInSubTree;
        }

        public void SaveTree()
        {
            GetHandler().SaveHandler();
        }
    }
}