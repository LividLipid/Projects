namespace ConsoleMenuTDD
{
    
    public abstract class TreeSaver
    {
        public abstract bool SaveTree(MenuItem tree, string filePath);
    }

    public class BinarySerializer : TreeSaver
    {
        // Singleton implementation.
        private static BinarySerializer _instance = new BinarySerializer();
        private BinarySerializer() { }

        public static BinarySerializer Instance { get { return _instance; } }

        public override bool SaveTree(MenuItem tree, string filePath)
        {
            return true;
        }
    }
}