using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Menu
{
    [Serializable]
    public class ItemLeaf : Item
    {
        public ItemLeaf(string title) : base(title)
        {
            Parent = new ItemSentinel("Sentinel");
        }

        public override void AddChild(Item child)
        {
            throw new Exception("A leaf has no children.");
        }

        public override Item GetChild(int i)
        {
            return new ItemSentinel("ItemSentinel");
        }

        public override void RemoveChild(int i)
        {
            throw new Exception("A leaf has no children.");
        }

        public override void RemoveChild(Item item)
        {
            throw new Exception("A leaf has no children.");
        }

        public override bool IsSentinel()
        {
            return false;
        }

        public override bool IsLeaf()
        {
            return true;
        }

        public override bool IsMenu()
        {
            return false;
        }

        public override List<ItemLeaf> GetSubTreeLeaves()
        {
            return new List<ItemLeaf>() { this };
        }

        public override UIData GetDataStructure()
        {
            return new UIDataLeaf(Title);
        }

        public override string GetItemTypeName()
        {
            return "Blank leaf";
        }
    }
}