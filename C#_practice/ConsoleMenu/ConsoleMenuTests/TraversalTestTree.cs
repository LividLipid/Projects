using System.Collections.Generic;
using ConsoleMenuTDD;

namespace ConsoleMenuTests
{
    public class TraversalTestTree
    {
        // This class defines a test tree which can be used for testing
        // traversal algorithms. It also contains correct sequences for
        // several different slgorithms.
        public Menu Root;
        public List<string> CorrectPreOrder;
        public List<string> CorrectInOrder;
        public List<string> CorrectPostOrder;
        public List<string> CorrectLevelOrder;

        //      Tree structure:
        //              F
        //         B        G
        //     A      D       I
        //          C   E       H

        public TraversalTestTree()
        {
            var a = MenuItemFactory.Create(typeof(Menu), "A");
            var b = MenuItemFactory.Create(typeof(Menu), "B");
            var c = MenuItemFactory.Create(typeof(Leaf), "C");
            var d = MenuItemFactory.Create(typeof(Menu), "D");
            var e = MenuItemFactory.Create(typeof(Leaf), "E");
            var f = MenuItemFactory.Create(typeof(Menu), "F");
            var g = MenuItemFactory.Create(typeof(Menu), "G");
            var h = MenuItemFactory.Create(typeof(Leaf), "H");
            var i = MenuItemFactory.Create(typeof(Menu), "I");

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
    }
}