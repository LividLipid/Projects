using System;
using ConsoleMenuTDD;

namespace ConsoleMenuTests
{
    [Serializable]
    public class StubHandler : Handler
    {
        public bool HasBeenSaved = false;

        public override string GetFilePath()
        {
            return "";
        }

        public override void SaveHandler()
        {
            HasBeenSaved = true;
        }
    }
}