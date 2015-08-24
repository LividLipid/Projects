using System;

namespace ConsoleMenuTDD
{
    [Serializable]
    public abstract class UserInterface
    {
        
        public abstract void Show(Item item);
    }
}