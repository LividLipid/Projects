using System;
using Menu;

namespace MenuTests
{
    [Serializable]
    public class StubSaver : Saver
    {
        public bool HasSaved = false;
        public bool HasLoaded = false;
        public override void SaveHandler(HandlerMenu handler, string filePath)
        {
            HasSaved = true;
        }

        public override HandlerMenu LoadHandler(string filePath)
        {
            HasLoaded = true;
            return new HandlerMenu();
        }
    }
}