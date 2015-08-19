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
            var mainMenu = new Menu("TestRoot");
            var subMenu1 = new Menu("TestSub1");
            var subMenu2 = new Menu("TestSub2");
            var subMenu3 = new Menu("TestSub3");
            var subMenu4 = new Menu("TestSub4");
            var subMenu5 = new Menu("TestSub5");

            var testTree = new MenuTree("Test Tree", mainMenu);
            testTree.AddMenuItem(subMenu1);
            testTree.AddMenuItem(subMenu2);

            testTree.PrintEntireTree();



            //var mainMenu = new Menu("TestRoot");
            //var menuTree = new TreeNode<Menu>(mainMenu);

            //var subMenu1 = new Menu("TestSub1");
            //var subMenu2 = new Menu("TestSub2");
            //var subMenu3 = new Menu("TestSub3");
            //var subMenu4 = new Menu("TestSub4");
            //var subMenu5 = new Menu("TestSub5");
            //menuTree.AddChild(subMenu1);
            //menuTree.AddChild(subMenu2);

            ////Console.WriteLine(menuTree.GetChild(1).Data.Title);
            ////Console.WriteLine(menuTree.GetChild(2).Data.Title);

            ////menuTree.RemoveChild(1);
            ////Console.WriteLine(menuTree.GetChild(1).Data.Title);
            ////Console.WriteLine(menuTree.GetChild(2).Data.Title);

            //var currentMenu = menuTree.GetChild(1);
            //currentMenu.AddChild(subMenu3);

            //menuTree.Traverse(menuTree, (x) => Console.WriteLine(x.Title));

            ////var gotten = menuTree.GetChild(1);
            ////Console.WriteLine(gotten.data.Title);



            //var entries = new List<string>() { "Test1", "Test2", "Test3" };
            //var entriesCnt = entries.Count;
            //var firstEntry = 1;
            //var quitSelection = entriesCnt + firstEntry;

            //var selection = firstEntry;
            //do
            //{
            //    Console.Clear();

            //    for (var i = 1; i < entriesCnt + firstEntry; i++)
            //    {
            //        var line = "[" + i + "] " + entries[i - firstEntry];
            //        if (i==selection)
            //        {
            //            WriteSelectedLine(line);
            //        }
            //        else
            //        {
            //            WriteNormalLine(line);
            //        }

            //    }
            //    Console.WriteLine("[" + quitSelection + "] " + "Quit");

            //    ConsoleKeyInfo cki = Console.ReadKey(true);
            //    ConsoleKey keyPress = cki.Key;
            //    char keyChar = cki.KeyChar;

            //    bool isDigit = char.IsDigit(keyChar);
            //    bool isChoiceMade = false;

            //    if (isDigit)
            //    {
            //        int numValue = (int) char.GetNumericValue(keyChar);
            //        isChoiceMade = (numValue >= firstEntry) && (numValue <= quitSelection);
            //        if (isChoiceMade)
            //        {
            //            selection = numValue;
            //        }
            //    }
            //    else
            //    {
            //        switch (keyPress)
            //        {
            //            case ConsoleKey.Enter:
            //                break;
            //            case ConsoleKey.Escape:
            //                break;
            //            case ConsoleKey.Backspace:
            //                break;
            //            case ConsoleKey.UpArrow:
            //                break;
            //            case ConsoleKey.DownArrow:
            //                break;
            //            case ConsoleKey.LeftArrow:
            //                break;
            //            case ConsoleKey.RightArrow:
            //                break;

            //        }
            //    }

            //} while (selection != quitSelection);




        }

        static void WriteNormalLine(string line)
        {
            Console.WriteLine(line);
        }
        static void WriteSelectedLine(string line)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}
