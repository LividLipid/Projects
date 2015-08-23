using System;

namespace ConsoleMenuTDD
{
    [Serializable]
    public abstract class UserInterface
    {
        public enum Commands
        {
            DoNothing = 0,
            Return = -1,
            Save = -1
        }
        public abstract void ShowMenuItem(Item item);
    }
}