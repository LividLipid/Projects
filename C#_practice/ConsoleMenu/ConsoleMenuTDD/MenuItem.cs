namespace ConsoleMenuTDD
{
    public abstract class MenuItem
    {


        public string Title { get; }
        public Menu Parent { get; set; }

        protected MenuItem(string title)
        {
            Title = title;
        }

        public abstract void AddChild(MenuItem child);
        public abstract MenuItem GetChild(int i);
        public abstract void RemoveChild(int i);
        //public abstract MenuItem DisplayAndReturnNextMenu();
        public abstract bool IsRoot();

        public Menu GetRoot()
        {
            var i = new IteratorGetRoot(this);
            while (!i.IsDone())
                i.Next();
            return (Menu) i.CurrentItem();
        }
    }
}