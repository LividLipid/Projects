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
            _treeName = name;
            SetTreeRoot(new ItemMenu("Main Menu"));
        }

        public HandlerMenu(string name, Item tree)
        {
            _treeName = name;
            SetTreeRoot(tree);
        }

        public HandlerMenu(string name, Item tree, UserInterface ui, Saver saver)
        {
            _treeName = name;
            SetTreeRoot(tree);
            _ui = ui;
            _saver = saver;
        }

        public override string GetFilePath()
        {
            return _folderPath + @"\" + _treeName;
        }

        public override void SaveHandler()
        {
            if (_saver == null)
                throw new Exception("Saver has not been set.");
            _saver.SaveHandler(this, GetFilePath());
        }

        public static HandlerMenu LoadHandler(Saver saver, string filePath)
        {
            return saver.LoadHandler(filePath);
        }
    }
}