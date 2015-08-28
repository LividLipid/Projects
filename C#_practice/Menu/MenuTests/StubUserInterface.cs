using System;
using MenuSystem;

namespace MenuTests
{
    [Serializable]
    public class StubUserInterface : UserInterface
    {
        public bool HasBeenShown = false;

        public override void Show(Handler handler, UIData data)
        {
            HasBeenShown = true;
        }
    }
}