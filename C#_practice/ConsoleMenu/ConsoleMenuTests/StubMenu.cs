using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenuTDD
{
    public class StubMenu : Menu
    {
        public bool HasBeenShown = false;

        public StubMenu(string title) : base(title)
        {
        }

        public override void ShowMenuItem()
        {
            HasBeenShown = true;
        }
    }
}