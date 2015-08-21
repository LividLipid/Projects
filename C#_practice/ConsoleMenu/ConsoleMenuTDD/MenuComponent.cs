namespace ConsoleMenuTDD
{
    public abstract class MenuComponent
    {


        public string Title { get; }
        public MenuComposite Parent { get; set; }
        //public bool IsRoot { get; }

        protected MenuComponent(string title)
        {
            Title = title;
        }

        public abstract void AddChild(MenuComponent child);
        public abstract MenuComponent GetChild(int i);
        public abstract void RemoveChild(int i);
        //public abstract MenuComponent DisplayAndReturnNextMenu();
    }
}