using System;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class ItemSentinel : Item
    {
        public ItemSentinel(string title) : base(title)
        {
        }

        public override void AddChild(Item child)
        {
        }

        public override Item GetChild(int i)
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