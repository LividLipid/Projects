using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleMenuTDD
{

    [Serializable]
    public abstract class TreeSaver
    {
        public abstract bool SaveTree(MenuItem tree, string filePath);
        public abstract MenuItem LoadTree(string filePath);
    }

    [Serializable]
    public class BinarySerializer : TreeSaver
    {
        // Singleton implementation.
        private static BinarySerializer _instance = new BinarySerializer();
        private BinarySerializer() { }

        public static BinarySerializer Instance { get { return _instance; } }

        public override bool SaveTree(MenuItem tree, string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, tree);
            stream.Close();

            return true;
        }

        public override MenuItem LoadTree(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("File does not exist.");
            else
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                MenuItem loadedTree = (MenuItem)formatter.Deserialize(stream);
                stream.Close();

                return loadedTree;
            }

        }
    }

    [Serializable]
    public class StubSaver : TreeSaver
    {
        // Singleton implementation.
        private static StubSaver _instance = new StubSaver();
        private StubSaver() { }

        public static StubSaver Instance { get { return _instance; } }

        public override bool SaveTree(MenuItem tree, string filePath)
        {
            return true;
        }

        public override MenuItem LoadTree(string filePath)
        {
            return MenuItemFactory.Create(typeof(MenuItemSentinel),"MenuItemSentinel");
        }
    }
}