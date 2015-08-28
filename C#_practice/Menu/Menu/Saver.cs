using System;

namespace Menu
{

    [Serializable]
    public abstract class Saver
    {
        public abstract void SaveHandler(HandlerMenu handler, string filePath);
        public abstract HandlerMenu LoadHandler(string filePath);
    }
}