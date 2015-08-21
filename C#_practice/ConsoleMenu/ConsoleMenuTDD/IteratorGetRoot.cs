namespace ConsoleMenuTDD
{
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