using System;

namespace MenuSystem
{

    [Serializable]
    public abstract class Saver
    {
        public abstract void SaveHandler(MenuHandler menuHandler, string filePath);
        public abstract MenuHandler LoadHandler(string filePath);
    }
}