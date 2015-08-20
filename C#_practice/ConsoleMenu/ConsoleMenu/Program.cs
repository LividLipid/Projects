using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
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
            var subMenu1 = new MenuParent("Submenu 1", mainMenu);
            subMenu1.AddChild(new MenuParent("subsubA", subMenu1));
            subMenu1.AddChild(new MenuParent("subsubB", subMenu1));

            mainMenu.AddChild(subMenu1);
            mainMenu.AddChild(new MenuParent("Submenu 2", mainMenu));
            mainMenu.AddChild(new MenuParent("Submenu 3", mainMenu));
            mainMenu.AddChild(new MenuLeafRSS("DR RSS", mainMenu, "http://www.dr.dk/nyheder/service/feeds/allenyheder"));


            mainMenu.Act();

        }
    }
}
