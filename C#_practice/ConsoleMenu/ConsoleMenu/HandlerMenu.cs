using System;

namespace ConsoleMenu
{
    [Serializable]
    public class HandlerMenu : Handler
    {
        public HandlerMenu()
        {

        }

        public HandlerMenu(string name)
        {
            TreeName = name;
            SetTreeRoot(new ItemMenu("Main Menu"));
        }

        public HandlerMenu(string name, Item tree)
        {
            TreeName = name;
            SetTreeRoot(tree);
        }

        public HandlerMenu(string name, Item tree, UserInterface ui, Saver saver)
        {
            TreeName = name;
            SetTreeRoot(tree);
            Ui = ui;
            Saver = saver;
        }

        public override string GetFilePath()
        {
            return FolderPath + @"\" + TreeName;
        }

        public override void SaveHandler()
        {
            if (Saver == null)
                throw new Exception("Saver has not been set.");
            Saver.SaveHandler(this, GetFilePath());
        }

        public static HandlerMenu LoadHandler(Saver saver, string filePath)
        {
            return saver.LoadHandler(filePath);
        }
    }
}