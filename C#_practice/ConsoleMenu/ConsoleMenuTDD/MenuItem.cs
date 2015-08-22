namespace ConsoleMenuTDD
{
    public abstract class MenuItem
    {


        public string Title { get; }
        public Menu Parent { get; set; }
        public int ChildrenCount = 0;

        protected MenuItem(string title)
        {
            Title = title;
        }

        public abstract void AddChild(MenuItem child);
        public abstract MenuItem GetChild(int i);
        public abstract void RemoveChild(int i);
        //public abstract MenuItem DisplayAndReturnNextMenu();
        public abstract bool IsRoot();

        public virtual bool IsSentinel()
        {
            return false;
        }

        public Menu GetRoot()
        {
            var i = new IteratorParentWalk(this);
            return (Menu) i.GetFinal();
        }
    }
}