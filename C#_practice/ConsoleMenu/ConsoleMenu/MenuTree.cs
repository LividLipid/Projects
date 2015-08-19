using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleMenu
{
    [Serializable]
    public class MenuTree
    {
        public string TreeTitle;
        private TreeNode<IMenuItem> Tree;

        public MenuTree() {}

        public MenuTree(string title, IMenuItem root)
        {
            TreeTitle = title;
            Tree = new TreeNode<IMenuItem>(root);
        }

        public static string GenerateFilePath(string folderPath, string title)
        {
            string filePath = folderPath + @"\" + title;
            return filePath;
        }

        public static MenuTree LoadTree(string folderPath, string title)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(GenerateFilePath(folderPath, title), FileMode.Open, FileAccess.Read, FileShare.Read);
            MenuTree loadedTree = (MenuTree)formatter.Deserialize(stream);
            stream.Close();
            return loadedTree;
        }

        public void SaveTree(string folderPath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(GenerateFilePath(folderPath, TreeTitle), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public void AddMenuItem(IMenuItem item)
        {
            Tree.AddChild(item);
        }

        public void RemoveMenuItem(int i)
        {
            Tree.RemoveChild(i);
        }

        public void PrintEntireTree()
        {
            Tree.Traverse(Tree, x => Console.WriteLine(x.Title));
        }
    }
}