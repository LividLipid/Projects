using System;

namespace ConsoleMenu
{
    [Serializable]
    public class MenuLeaf : MenuNode
    {
        public MenuLeaf(string title, MenuNode parent) : base(title, parent)
        {

        }
        public override void Act()
        {

        }
    }
}