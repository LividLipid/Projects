using System;
using Menu;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    [Serializable]
    public class UserInterfaceConsole : UserInterface
    {

        public override void Show(Handler handler, UIData data)
        {
            if (data.GetType() == typeof(UIDataMenu))
                new ConsoleScreenMenuMain(handler, (UIDataMenu) data).Display();
            if (data.GetType() == typeof(UIDataLeaf))
                new ConsoleScreenLeaf(handler, (UIDataLeaf)data).Display();
            if (data.GetType() == typeof(UIDataNewItem))
                new ConsoleScreenMenuAddNew(handler, (UIDataNewItem)data).Display();
        }
    }
}