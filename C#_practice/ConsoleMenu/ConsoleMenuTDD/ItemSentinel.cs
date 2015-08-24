using System;
using System.Collections.Generic;

namespace ConsoleMenuTDD
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

        public override bool IsSentinel()
        {
            return true;
        }

        public override bool IsLeaf()
        {
            return false;
        }

        public override List<Leaf> GetSubTreeLeaves()
        {
            throw new Exception("A sentinel has no leaves.");
        }
    }
}