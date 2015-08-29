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
            handler.DisplayMenu();

        }

        static MenuHandler CreateExampleHandler()
        {
            var name = "Default Menu System";
            var tree = new ExampleTree().Root;
            var handler = new MenuHandler(name, tree);

            var menuController = new MenuController(handler);
            var ui = new ConsoleUserInterface(menuController);
            handler.SetUserInterface(ui);

            var saver = new SaverBinarySerializer();
            handler.SetSaver(saver);

            return handler;
        }
    }
}
