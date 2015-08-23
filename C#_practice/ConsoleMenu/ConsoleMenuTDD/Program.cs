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

            var testTree = new TreeExample();
            var targetNode = new Menu("hej");
            var originNode = testTree.ListOfNodes.Last();

            Console.WriteLine(originNode.HasInTree(targetNode));
        }
    }
}
