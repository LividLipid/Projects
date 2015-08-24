using System;
using ConsoleMenu;

namespace ConsoleMenuTests
{
    [Serializable]
    public class StubUserInterface : UserInterface
    {
        public bool HasBeenShown = false;

        public override void Show(Handler handler, Data data)
        {
            HasBeenShown = true;
        }
    }
}