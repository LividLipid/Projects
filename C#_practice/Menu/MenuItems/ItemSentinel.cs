using System;
using System.Collections.Generic;

namespace MenuItems
{
    [Serializable]
    public class ItemSentinel : Item
    {
        public ItemSentinel(string title) : base(title)
        {
            Parent = null;
        }

        public override void AddChild(Item child)
        {
        }

        public override Item GetChild(int i)
        {
            return this;
        }

        public override void RemoveChild(int i)
        {
        }

        public override void RemoveChild(Item item)
        {
        }

        public override bool IsSentinel()
        {
            return true;
        }

        public override bool IsLeaf()
        {
            return false;
        }

        public override bool IsMenu()
        {
            return false;
        }

        public override List<ItemLeaf> GetSubTreeLeaves()
        {
            throw new Exception("A sentinel has no leaves.");
        }
    }
}