using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class Menu : Item
    {
        private readonly List<Item> _children = new List<Item>();

        public Menu(string title) : base(title)
        {
            Parent = new ItemSentinel("Sentinel");
        }

        public override void AddChild(Item child)
        {
            if (this.HasInTree(child))
                throw new ArgumentException("Child already exists in Tree.");
            else
            {
                child.Parent = this;
                _children.Add(child);
                ChildrenCount++;
            }
        }

        public override Item GetChild(int i)
        {
            return _children[i];
        }

        public override void RemoveChild(int i)
        {
            var hasNoChildren = ChildrenCount < 1;
            var isOutOfRange = (i < 0) || (i > ChildrenCount-1);
            if (isOutOfRange || hasNoChildren) return;
            _children.RemoveAt(i);
            ChildrenCount--;
        }

        public override bool IsSentinel()
        {
            return false;
        }

        public List<string> GetChildrenTitles()
        {
            var titles = _children.Select(child => child.Title).ToList();
            return titles;
        }
    }
}