using System;
using System.Collections.Generic;
using System.Linq;

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
            var data = item.GetDataStructure();
            _ui.Show(this, data);

            ExecuteNext();
        }

        private void ExecuteNext()
        {
            var nextCommand = CommandsToExecute.Dequeue();
            nextCommand.Execute();
        }

        public void ExecuteRefreshCommand()
        {
            ShowItem(_currentItem);
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

        public void ExecuteSelectNewItemCommand()
        {
            var creatableTypes = Item.GetCreatableItemTypes();
            var data = new UIDataNewItem("Add new item", creatableTypes);
            _ui.Show(this, data);

            ExecuteNext();
        }

        public void ExecuteAddNewItemCommand(Type type, string title)
        {
            var itemToAdd = ItemFactory.Create(type, title);
            _currentItem.AddChild(itemToAdd);

            ShowItem(_currentItem);
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