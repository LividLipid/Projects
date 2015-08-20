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
            subMenu1.AddChild(new MenuParent("subsubA", mainMenu));
            subMenu1.AddChild(new MenuParent("subsubB", mainMenu));

            mainMenu.AddChild(subMenu1);
            mainMenu.AddChild(new MenuParent("Submenu 2", mainMenu));
            mainMenu.AddChild(new MenuParent("Submenu 3", mainMenu));

            MenuParent currentMenu = mainMenu;
            bool quitIsChosen = false;
            bool leafIsChosen = false;
            do
            {
                MenuUI testUI = new MenuUI(currentMenu);
                int choice = testUI.ShowMenu();

                if (choice == -1)
                {
                    if (currentMenu == mainMenu)
                        quitIsChosen = true;
                    else
                        currentMenu = (MenuParent) currentMenu.Parent;
                }
                else
                {
                    var chosenMenuItem = currentMenu.GetMenuChild(choice);
                    if (chosenMenuItem.GetType() == typeof (MenuLeaf))
                    {
                        MenuLeaf chosenLeaf = (MenuLeaf) chosenMenuItem;
                        leafIsChosen = true;
                    }
                    else if (chosenMenuItem.GetType() == typeof(MenuParent))
                    {
                        MenuParent chosenSubMenu = (MenuParent) chosenMenuItem;
                        if (chosenSubMenu.GetChildrenTitles().Count > 0)
                            currentMenu = chosenSubMenu;
                    }
                }
                    

            } while (!quitIsChosen && !leafIsChosen);
            
        }
    }
}
