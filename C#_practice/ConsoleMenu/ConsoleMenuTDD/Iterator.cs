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
    }

    public class IteratorGetRoot : Iterator
    {
        public IteratorGetRoot(MenuItem item) : base(item)
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
}