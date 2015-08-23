using System;
using System.Diagnostics;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class UserInterfaceConsole : UserInterface
    {
        // Singleton implementation.
        private static UserInterfaceConsole _instance = new UserInterfaceConsole();
        private UserInterfaceConsole() { }

        public static UserInterfaceConsole Instance { get { return _instance; } }

        public override void ShowMenuItem(Item item)
        {
            var typeName = item.GetType().Name;
            switch (typeName)
            {
                case "Menu":
                    var ui = new UserInterfaceConsoleMenu();
                    ui.ShowMenu((Menu)item);
                    break;
                default:
                    throw new ArgumentException("Unknown item type.");
            }
        }
    }
}