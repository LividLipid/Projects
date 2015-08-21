namespace ConsoleMenuTDD
{
    public abstract class MenuComponent
    {


        public string Title { get; }

        public MenuComponent(string title)
        {
            Title = title;
        }

        public abstract void AddChild(MenuComponent child);
        public abstract MenuComponent GetChild(int i);
        public abstract void RemoveChild(int i);
    }
}