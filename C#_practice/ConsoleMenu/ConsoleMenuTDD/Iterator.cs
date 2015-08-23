using System.Collections.Generic;

namespace ConsoleMenuTDD
{
    public abstract class Iterator
    {
        protected Item Item;

        protected Iterator(Item item)
        {
            Item = item;
        }

        public abstract Item First();
        public abstract Item Next();
        public abstract bool IsDone();
        public abstract Item CurrentItem();

        public Item GetFinal()
        {
            while (!IsDone())
                Next();
            return CurrentItem();
        }

        public Item SearchForTitle(string targetTitle)
        {
            while (!IsDone())
            {
                if (CurrentItem().Title.Equals(targetTitle))
                    return CurrentItem();
                Next();
            }
            return ItemFactory.Create(typeof (ItemSentinel), "ItemSentinel");
        }
    }
}