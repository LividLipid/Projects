using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConsoleMenu
{
    [Serializable]
    public class Leaf : Item
    {
        public Leaf(string title) : base(title)
        {
            Parent = new ItemSentinel("Sentinel");
        }

        public override void AddChild(Item child)
        {
            throw new Exception("Cannot assign child to leaf.");
        }

        public override Item GetChild(int i)
        {
            return new ItemSentinel("ItemSentinel");
        }

        public override void RemoveChild(int i)
        {
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

        public override List<Leaf> GetSubTreeLeaves()
        {
            return new List<Leaf>() { this };
        }
    }
}