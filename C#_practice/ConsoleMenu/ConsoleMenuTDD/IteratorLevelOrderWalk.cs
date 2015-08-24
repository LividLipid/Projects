using System.Collections.Generic;

namespace ConsoleMenuTDD
{
    public class IteratorLevelOrderWalk : Iterator
    {
        private readonly Queue<Item> _walkSequence = new Queue<Item>();

        public IteratorLevelOrderWalk(Item item) : base(item)
        {
            // Breadth first search using a queue.
            var q = new Queue<Item>();
            q.Enqueue(Item);
            while (q.Count > 0)
            {
                Item current = q.Dequeue();
                _walkSequence.Enqueue(current);

                var i = 0;
                while (i < current.ChildrenCount)
                {
                    q.Enqueue(current.GetChild(i));
                    i++;
                }
            }
        }

        public override Item First()
        {
            return Item;
        }

        public override Item Next()
        {
            return _walkSequence.Dequeue();
        }

        public override bool IsDone()
        {
            return (_walkSequence.Count <= 0);
        }

        public override Item CurrentItem()
        {
            return _walkSequence.Peek();
        }
    }
}