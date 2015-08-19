using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleMenu
{
    [Serializable]
    public class MenuParent : MenuNode
    {
        private LinkedList<MenuNode> Children;

        public MenuParent(string title, MenuNode parent) : base(title, parent)
        {
            Children = new LinkedList<MenuNode>();
        }

        public void AddChild(MenuNode child)
        {
            Children.AddLast(child);
            MenuItemCount++;
        }

        public void RemoveChild(int i)
        {
            foreach (MenuNode n in Children)
                if (--i == 0)
                {
                    Children.Remove(n);
                }
        }

        public MenuNode GetMenuChild(int i)
        {
            foreach (MenuNode n in Children)
                if (--i == 0)
                    return n;
            return null;
        }

        public List<string> GetChildrenTitles()
        {
            var titleList = new List<string>(MenuItemCount);
            foreach (var n in Children)
            {
                titleList.Add(n.Title);
            }
            return titleList;
        }

        public void PrintTree()
        {
            TraverseTree(this, x => Console.WriteLine(x.Title));
        }

        private void TraverseTree(MenuParent parent, Action<MenuParent> visitor)
        {
            visitor(parent);
            foreach (MenuParent kid in parent.Children)
                TraverseTree(kid, visitor);
        }

        public static string GenerateFilePath(string folderPath, string title)
        {
            string filePath = folderPath + @"\" + title;
            return filePath;
        }

        public static MenuNode LoadTree(string folderPath, string title)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(GenerateFilePath(folderPath, title), FileMode.Open, FileAccess.Read, FileShare.Read);
            MenuNode loadedTree = (MenuNode)formatter.Deserialize(stream);
            stream.Close();
            return loadedTree;
        }

        public void SaveTree(string folderPath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(GenerateFilePath(folderPath, Title), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }


    }
}