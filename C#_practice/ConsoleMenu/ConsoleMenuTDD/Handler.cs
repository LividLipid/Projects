using System;

namespace ConsoleMenuTDD
{
    [Serializable]
    public abstract class Handler
    {
        private Item _treeRoot;
        public string _treeName;
        public UserInterface _ui;
        public Saver _saver;
        public string _folderPath;

        public void SetTreeRoot(Item tree)
        {
            _treeRoot = tree;
            _treeRoot.TreeHandler = this;
        }

        public Item GetTreeRoot()
        {
            return _treeRoot;
        }

        public abstract string GetFilePath();
        public abstract void SaveHandler();
    }
}