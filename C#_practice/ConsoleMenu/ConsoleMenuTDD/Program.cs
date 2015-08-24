using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsoleMenu
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tree = new ExampleTree().Root;
            var handler = new HandlerMenu("Test", tree);

            handler.ShowTree();
        }
    }
}
