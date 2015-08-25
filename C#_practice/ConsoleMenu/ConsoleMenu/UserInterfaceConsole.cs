using System;

namespace ConsoleMenu
{
    [Serializable]
    public class UserInterfaceConsole : UserInterface
    {

        public override void Show(Handler handler, Data data)
        {
            
            if (data.GetType() == typeof(DataMenu))
            {
                var ui = new UserInterfaceConsoleMenu(handler, (DataMenu) data);
                ui.DisplayMenu();
            }
            if (data.GetType() == typeof(DataLeaf))
            {
                var ui = new UserInterfaceConsoleLeaf(handler, (DataLeaf)data);
                ui.DisplayLeaf();
            }
        }
    }
}