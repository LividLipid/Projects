using System;

namespace ConsoleMenuTDD
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