using System;

namespace Menu
{

    [Serializable]
    public abstract class Saver
    {
        public abstract void SaveHandler(Handler handler, string filePath);
        public abstract Handler LoadHandler(string filePath);
    }
}