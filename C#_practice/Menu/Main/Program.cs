using Menu;

namespace Main
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
