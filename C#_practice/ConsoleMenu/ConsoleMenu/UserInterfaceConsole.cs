using System;

namespace ConsoleMenu
{
    [Serializable]
    public class UserInterfaceConsole : UserInterface
    {

        

        public override void Show(Handler handler, Data data)
        {
            
            if (data.GetType() == typeof (DataMenu))
            {
                var ui = new UserInterfaceConsoleMenu(handler, (DataMenu) data);
                ui.Display_Menu();
            }
        }
    }
}