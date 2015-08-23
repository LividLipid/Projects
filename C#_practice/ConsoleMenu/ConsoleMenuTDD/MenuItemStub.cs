using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenuTDD
{
    public class MenuItemStub : MenuItem
    {
        public bool HasBeenShown = false;

        public MenuItemStub(string title) : base(title)
        {
        }

        public override void AddChild(MenuItem child)
        {
            throw new NotImplementedException();
        }

        public override MenuItem GetChild(int i)
        {
            throw new NotImplementedException();
        }

        public override void RemoveChild(int i)
        {
            throw new NotImplementedException();
        }

        public override bool IsRoot()
        {
            throw new NotImplementedException();
        }

        public override void ShowMenuItem()
        {
            //HasBeenShown = true;
        }
    }
}