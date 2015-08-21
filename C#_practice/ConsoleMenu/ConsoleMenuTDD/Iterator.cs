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
}