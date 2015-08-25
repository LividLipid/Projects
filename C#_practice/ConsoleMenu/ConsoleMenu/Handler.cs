using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    [Serializable]
    public abstract class Handler
    {
        public static string DefaultFolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";
        private Item _treeRoot;
        private Item _currentItem;
        public string _treeName;
        public UserInterface _ui = new UserInterfaceConsole();
        public Saver _saver = new SaverBinarySerializer();
        public string _folderPath = DefaultFolderPath;

        public Queue<Command> CommandsToExecute = new Queue<Command>();
        public List<Command> ExecutedCommands = new List<Command>();
        

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

        public void ShowTree()
        {
            ShowItem(_treeRoot);
        }
        public void ShowItem(Item item)
        {
            _currentItem = item;

            if (item.IsMenu())
            {
                var menuItem = (Menu) item;
                var itemData = new DataMenu()
                {
                    MenuTitle = menuItem.Title,
                    ChildrenTitles = menuItem.GetChildrenTitles()
                };
                _ui.Show(this, itemData);

                var nextCommand = CommandsToExecute.Dequeue();
                nextCommand.Execute();
            }
        }

        public void ExecuteQuitCommand()
        {
            ForgetPreviousCommands();
            Environment.Exit(0);
        }

        public void ExecuteReturnCommand()
        {
            ForgetPreviousCommands();
            if (!_currentItem.IsRoot())
                ShowItem(_currentItem.Parent);
            else
                ExecuteQuitCommand();
        }

        public void ExecuteSaveCommand()
        {

        }

        public void ExecuteSelectCommand(int selection)
        {
            ForgetPreviousCommands();
            var selectedItem = _currentItem.GetChild(selection);

            ShowItem(selectedItem);
        }

        private void ForgetPreviousCommands()
        {
            ExecutedCommands = new List<Command>();
    }
    }
}