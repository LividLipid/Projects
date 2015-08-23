using System;

namespace ConsoleMenuTDD
{
    [Serializable]
    public class HandlerMenu : Handler
    {
        private Item _treeRoot;
        public string _treeName;
        public UserInterface _ui = new UserInterfaceConsole();
        public Saver _saver = new SaverBinarySerializer();
        public string _folderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";

        public HandlerMenu()
        {

        }

        public HandlerMenu(string name)
        {
            _treeName = name;
            SetTreeRoot(new Menu("Main Menu"));
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

        //public virtual void ShowMenuItem()
        //{
        //    ExecuteUserInterfaceOperation(this);
        //}

        //private void ExecuteUserInterfaceOperation(Item item)
        //{
        //    if (_ui == null)
        //        throw new Exception("User interface has not been set.");
        //    _ui.ShowMenuItem(item);
        //}
    }
}