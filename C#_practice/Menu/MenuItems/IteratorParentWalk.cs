namespace MenuItems
{
    public class IteratorParentWalk : Iterator
    {
        public IteratorParentWalk(Item item) : base(item)
        {
        }

        public override Item First()
        {
            return Item;
        }

        public override Item Next()
        {
            Item = Item.Parent;
            return Item;
        }

        public override bool IsDone()
        {
            return Item.IsRoot();
        }

        public override Item CurrentItem()
        {
            return Item;
        }
    }
}