using System;
using NUnit.Framework;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class Leaf : MenuItem
    {
        public Leaf(string title) : base(title)
        {
        }

        public override void AddChild(MenuItem child)
        {
            throw new Exception("Cannot assign child to leaf.");
        }

        public override MenuItem GetChild(int i)
        {
            return new MenuItemSentinel("MenuItemSentinel");
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