using System;
using NUnit.Framework;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class Leaf : Item
    {
        public Leaf(string title) : base(title)
        {
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

        public override bool IsRoot()
        {
            return false;
        }
    }
}