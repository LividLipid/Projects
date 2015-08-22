using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleMenuTDD
{
    public class Menu : MenuItem
    {
        private readonly List<MenuItem> _children = new List<MenuItem>();

        public Menu(string title) : base(title)
        {
            // A new menu is its own parent until it becomes a submenu.
            Parent = this; 
        }

        public override void AddChild(MenuItem child)
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

        public override MenuItem GetChild(int i)
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

        public List<string> GetChildrenTitles()
        {
            var titles = _children.Select(child => child.Title).ToList();
            return titles;
        }

        public override bool IsRoot()
        {
            return this.Equals(Parent);
        }
    }
}