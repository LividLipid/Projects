using System.Collections.Generic;
using System.Linq;
using ConsoleMenu;

namespace ConsoleMenu
{
    public class ExampleTree
    {
        // This class defines a test tree which can be used for testing
        // traversal algorithms. It also contains correct sequences for
        // several different slgorithms.
        public Menu Root;
        public List<Item> ListOfNodes = new List<Item>();
        public List<Leaf> ListOfLeaves = new List<Leaf>();
        public List<string> CorrectPreOrder;
        public List<string> CorrectInOrder;
        public List<string> CorrectPostOrder;
        public List<string> CorrectLevelOrder;

        //      Tree structure:
        //              F
        //         B        G
        //     A      D       I
        //          C   E       H

        public ExampleTree()
        {
            var a = ItemFactory.Create(typeof(Menu), "A");
            var b = ItemFactory.Create(typeof(Menu), "B");
            var c = ItemFactory.Create(typeof(Leaf), "C");
            var d = ItemFactory.Create(typeof(Menu), "D");
            var e = ItemFactory.Create(typeof(Leaf), "E");
            var f = ItemFactory.Create(typeof(Menu), "F");
            var g = ItemFactory.Create(typeof(Menu), "G");
            var h = ItemFactory.Create(typeof(Leaf), "H");
            var i = ItemFactory.Create(typeof(Menu), "I");

            ListOfNodes.Add(a);
            ListOfNodes.Add(b);
            ListOfNodes.Add(c);
            ListOfNodes.Add(d);
            ListOfNodes.Add(e);
            ListOfNodes.Add(f);
            ListOfNodes.Add(g);
            ListOfNodes.Add(h);
            ListOfNodes.Add(i);

            ListOfLeaves.Add((Leaf) c);
            ListOfLeaves.Add((Leaf) e);
            ListOfLeaves.Add((Leaf) h);

            d.AddChild(c);
            d.AddChild(e);

            b.AddChild(a);
            b.AddChild(d);

            i.AddChild(h);
            g.AddChild(i);

            f.AddChild(b);
            f.AddChild(g);

            Root = (Menu) f;
            CorrectPreOrder = new List<string> { "F", "B", "A", "D", "C", "E", "G", "I", "H" };
            CorrectInOrder = new List<string> {"A", "B", "C", "D", "E", "F", "G", "H", "I"};
            CorrectPostOrder = new List<string> {"A", "C", "E", "D", "B", "H", "I", "G", "F"};
            CorrectLevelOrder = new List<string> {"F", "B", "G", "A", "D", "I", "C", "E", "H"};
        }

        public Leaf GetLeaf()
        {
            return (Leaf) ListOfLeaves.First();
        }
    }
}