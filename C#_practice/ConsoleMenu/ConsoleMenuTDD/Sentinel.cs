namespace ConsoleMenuTDD
{
    public class Sentinel : MenuItem
    {
        public Sentinel(string title) : base(title)
        {
        }

        public override void AddChild(MenuItem child)
        {
        }

        public override MenuItem GetChild(int i)
        {
            return this;
        }

        public override void RemoveChild(int i)
        {
        }

        public override bool IsRoot()
        {
            return false;
        }

        public override bool IsSentinel()
        {
            return true;
        }
    }
}