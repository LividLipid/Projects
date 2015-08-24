using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsoleMenuTDD
{
    public class Program
    {
        static void Main(string[] args)
        {
            var testTree = new ExampleTree();
            var knownLeaves = testTree.ListOfLeaves;
            var foundLeaves = testTree.Root.GetSubTreeLeaves();

            var areEqual = Enumerable.SequenceEqual(knownLeaves.OrderBy(t => t.Title), foundLeaves.OrderBy(t => t.Title));
            //Console.WriteLine(areEqual);
        }
    }
}
