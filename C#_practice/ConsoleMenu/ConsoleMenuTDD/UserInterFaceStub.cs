using System;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class UserInterfaceStub : UserInterface
    {
        public bool HasBeenShown = false;

        public override void ShowMenuItem(MenuItem item)
        {
            HasBeenShown = true;
        }
    }
}