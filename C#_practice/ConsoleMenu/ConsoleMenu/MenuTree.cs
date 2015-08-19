using System;

namespace ConsoleMenu
{
    public class MenuTree
    {
        private static string FolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";
        private string TreeTitle;
        private string FilePath;
        private TreeNode<IMenuItem> Tree;

        public MenuTree() {}

        public MenuTree(string title, IMenuItem root)
        {
            TreeTitle = title;
            FilePath = FolderPath + @"\" + TreeTitle;
            Tree = new TreeNode<IMenuItem>(root);
        }

        public void SaveTree()
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(Menu));
            var file = System.IO.File.Create(FilePath);
            writer.Serialize(file, this);
            file.Close();
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
            Tree.Traverse(Tree, (x) => Console.WriteLine(x.Title));
        }
    }
}