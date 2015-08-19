using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";

            var mainMenu = new MenuParent("Test Menu", null);
            mainMenu.AddChild(new MenuParent("Submenu 1", mainMenu));
            mainMenu.AddChild(new MenuParent("Submenu 2", mainMenu));
            mainMenu.AddChild(new MenuParent("Submenu 3", mainMenu));

            MenuUI testUI = new MenuUI();
            testUI.ShowMenu(mainMenu);
        }
    }
}
