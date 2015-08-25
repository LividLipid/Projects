using System;

namespace ConsoleMenu
{
    [Serializable]
    public class UserInterfaceConsole : UserInterface
    {

        public override void Show(Handler handler, Data data)
        {
            if (data.GetType() == typeof(DataMenu))
                new ConsoleScreenMenu(handler, (DataMenu) data).Display();
            if (data.GetType() == typeof(DataLeaf))
                new ConsoleScreenLeaf(handler, (DataLeaf)data).Display();
        }
    }
}