using System;
using ConsoleMenuTDD;

namespace ConsoleMenuTests
{
    [Serializable]
    public class StubUserInterface : UserInterface
    {
        public bool HasBeenShown = false;

        public override void ShowMenuItem(Item item)
        {
            HasBeenShown = true;
        }
    }
}