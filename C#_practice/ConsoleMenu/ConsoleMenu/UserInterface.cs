using System;

namespace ConsoleMenu
{
    [Serializable]
    public abstract class UserInterface
    {
        
        public abstract void Show(Handler handler, UIData data);
    }
}