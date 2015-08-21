using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenuTDD
{
    public class MenuComposite : MenuComponent
    {
        private readonly List<MenuComponent> _children = new List<MenuComponent>();
        public int ChildrenCount = 0;

        public MenuComposite(string title) : base(title)
        {
        }

        public override void AddChild(MenuComponent child)
        {
            _children.Add(child);
            ChildrenCount++;
        }

        public override MenuComponent GetChild(int i)
        {
            return _children[i];
        }

        public override void RemoveChild(int i)
        {
            bool IsInRange = (i >= 0) && (i <= _children.Count-1);
            if (IsInRange)
            {
                _children.RemoveAt(i);
                ChildrenCount--;
            }
        }

        public List<string> GetChildrenTitles()
        {
            var titles = _children.Select(child => child.Title).ToList();
            return titles;
        }
    }
}