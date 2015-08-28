using System.Collections.Generic;
using System.Linq;
using MenuItems;

namespace MenuSystem
{
    public class ExampleTree
    {
        // This class defines a test tree which can be used for testing
        // traversal algorithms. It also contains correct sequences for
        // several different slgorithms.
        public ItemMenu Root;
        public List<Item> ListOfNodes = new List<Item>();
        public List<ItemLeaf> ListOfLeaves = new List<ItemLeaf>();
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
            //var a = ItemFactory.Create(typeof(ItemLeaf), "A");
            var a = ItemFactory.Create(typeof(ItemMenu), "A");
            var b = ItemFactory.Create(typeof(ItemMenu), "B");
            var c = ItemFactory.Create(typeof(ItemLeaf), "C");
            var d = ItemFactory.Create(typeof(ItemMenu), "D");
            var e = ItemFactory.Create(typeof(ItemLeaf), "E");
            var f = ItemFactory.Create(typeof(ItemMenu), "F");
            var g = ItemFactory.Create(typeof(ItemMenu), "G");
            var h = ItemFactory.Create(typeof(ItemLeaf), "H");
            var i = ItemFactory.Create(typeof(ItemMenu), "I");

            ListOfNodes.Add(a);
            ListOfNodes.Add(b);
            ListOfNodes.Add(c);
            ListOfNodes.Add(d);
            ListOfNodes.Add(e);
            ListOfNodes.Add(f);
            ListOfNodes.Add(g);
            ListOfNodes.Add(h);
            ListOfNodes.Add(i);

            //ListOfLeaves.Add((ItemLeaf) a);
            ListOfLeaves.Add((ItemLeaf) c);
            ListOfLeaves.Add((ItemLeaf) e);
            ListOfLeaves.Add((ItemLeaf) h);

            d.AddChild(c);
            d.AddChild(e);

            b.AddChild(a);
            b.AddChild(d);

            i.AddChild(h);
            g.AddChild(i);

            f.AddChild(b);
            f.AddChild(g);

            Root = (ItemMenu) f;
            CorrectPreOrder = new List<string> { "F", "B", "A", "D", "C", "E", "G", "I", "H" };
            CorrectInOrder = new List<string> {"A", "B", "C", "D", "E", "F", "G", "H", "I"};
            CorrectPostOrder = new List<string> {"A", "C", "E", "D", "B", "H", "I", "G", "F"};
            CorrectLevelOrder = new List<string> {"F", "B", "G", "A", "D", "I", "C", "E", "H"};
        }

        public ItemLeaf GetLeaf()
        {
            return (ItemLeaf) ListOfLeaves.First();
        }
    }
}