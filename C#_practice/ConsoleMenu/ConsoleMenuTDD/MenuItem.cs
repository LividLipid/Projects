namespace ConsoleMenuTDD
{
    public abstract class MenuItem
    {


        // The title uniquely identifies a menuitem,
        // and the same item cannot exist twice in the same tree.
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

        public bool HasInTree(MenuItem item)
        {
            var root = GetRoot();
            var isInTree = root.HasInSubTree(item);

            return isInTree;
        }

        public bool HasInSubTree(MenuItem item)
        {
            var i = new IteratorLevelOrderWalk(this);
            var foundItem = i.SearchForTitle(item.Title);
            var isInSubTree = !foundItem.IsSentinel();

            return isInSubTree;
        }
    }
}