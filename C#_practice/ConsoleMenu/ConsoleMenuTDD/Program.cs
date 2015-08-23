using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuTDD
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainmenu = ItemFactory.Create(typeof(Menu), "mainmenu");
            var submenu = ItemFactory.Create(typeof(Menu), "submenu");

            mainmenu.AddChild(submenu);
            mainmenu.AddChild(submenu);
        }
    }
}
