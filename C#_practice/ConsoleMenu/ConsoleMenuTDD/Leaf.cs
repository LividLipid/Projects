namespace ConsoleMenuTDD
{
    public class Leaf : MenuItem
    {
        public Leaf(string title) : base(title)
        {
        }

        public override void AddChild(MenuItem child)
        {
        }

        public override MenuItem GetChild(int i)
        {
            return this; // A leaf is treated as its own child.
        }

        public override void RemoveChild(int i)
        {
        }

        public override bool IsRoot()
        {
            return false;
        }
    }
}