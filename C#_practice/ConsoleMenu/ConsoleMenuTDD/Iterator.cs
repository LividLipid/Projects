using System.Collections.Generic;

namespace ConsoleMenuTDD
{
    public abstract class Iterator
    {
        protected MenuItem Item;

        protected Iterator(MenuItem item)
        {
            Item = item;
        }

        public abstract MenuItem First();
        public abstract MenuItem Next();
        public abstract bool IsDone();
        public abstract MenuItem CurrentItem();

        public MenuItem GetFinal()
        {
            while (!IsDone())
                Next();
            return CurrentItem();
        }

        public MenuItem SearchForTitle(string targetTitle)
        {
            while (!IsDone())
            {
                if (CurrentItem().Title.Equals(targetTitle))
                    return CurrentItem();
                Next();
            }
            return MenuItemFactory.Create(typeof (MenuItemSentinel), "MenuItemSentinel");
        }
    }

    public class IteratorParentWalk : Iterator
    {
        public IteratorParentWalk(MenuItem item) : base(item)
        {
        }

        public override MenuItem First()
        {
            return Item;
        }

        public override MenuItem Next()
        {
            Item = Item.Parent;
            return Item;
        }

        public override bool IsDone()
        {
            return Item.IsRoot();
        }

        public override MenuItem CurrentItem()
        {
            return Item;
        }
    }

    public class IteratorLevelOrderWalk : Iterator
    {
        private Queue<MenuItem> _walkSequence = new Queue<MenuItem>();
             
        public IteratorLevelOrderWalk(MenuItem item) : base(item)
        {
            // Breadth first search using a queue.
            var q = new Queue<MenuItem>();
            q.Enqueue(Item);
            while (q.Count > 0)
            {
                MenuItem current = q.Dequeue();
                _walkSequence.Enqueue(current);

                var i = 0;
                while (i < current.ChildrenCount)
                {
                    q.Enqueue(current.GetChild(i));
                    i++;
                }
            }
        }

        public override MenuItem First()
        {
            return Item;
        }

        public override MenuItem Next()
        {
            return _walkSequence.Dequeue();
        }

        public override bool IsDone()
        {
            return (_walkSequence.Count <= 0);
        }

        public override MenuItem CurrentItem()
        {
            return _walkSequence.Peek();
        }
    }
}