using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;
using ConsoleInterface;
using MenuSystem;

namespace MainPartition
{
    class Program
    {
        public static void Main(string[] args)
        {
            var handler = CreateExampleHandler();
            var tree = new ExampleTree().Root;
            handler.DisplayMenu(tree);

        }

        static MenuHandler CreateExampleHandler()
        {
            var name = "Default Menu System";
            var handler = new MenuHandler(name);

            var menuController = new MenuController(handler);
            var ui = new ConsoleUserInterface(menuController);
            handler.SetUserInterface(ui);

            var saver = new SaverBinarySerializer();
            handler.SetSaver(saver);

            return handler;
        }
    }
}
