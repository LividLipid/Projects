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

        private Command _currentCommand;
        public Queue<Command> CommandsToExecute = new Queue<Command>();
        private List<Command> _undoableCommands = new List<Command>();
        private int _undoIndex = -1;
        

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
            ShowData(data);
        }

        public void ShowData(UIData data)
        {
            _ui.Show(this, data);
            ExecuteNext();
        }

        private void ExecuteNext()
        {
            _currentCommand = CommandsToExecute.Dequeue();
            if (_currentCommand.IsUndoable())
            {
                _undoableCommands.Add(_currentCommand);
                _undoIndex++;
            }

            _currentCommand.Execute();
        }

        public void ExecuteRefreshCommand()
        {
            ShowItem(_currentItem);
        }

        public void ExecuteQuitCommand()
        {
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

            ShowData(data);
        }

        public void ExecuteSelectCommand(int selection)
        {
            ForgetPreviousCommands();
            var selectedItem = _currentItem.GetChild(selection);

            ShowItem(selectedItem);
        }

        public void ExecuteAddNewItemCommand(Type type, string title)
        {
            var itemToAdd = ItemFactory.Create(type, title);
            _currentItem.AddChild(itemToAdd);
            _currentCommand.

            ShowItem(_currentItem);
        }

        public void UndoAddNewItemCommand(Handler Add)
        {
            
        }

        public void ExecuteRemoveItemCommand(int selection)
        {
            var removedItem = _currentItem.GetChild(selection);
            _currentItem.RemoveChild(selection);

            ShowItem(_currentItem);
        }

        public void ExecuteSaveCommand()
        {

        }

        public void Undo()
        {
            if (_undoIndex < 0) return;
            var cmd = (Undoable) _undoableCommands[_undoIndex];
            _undoIndex--;
            cmd.Unexecute();
        }

        private void ForgetPreviousCommands()
        {
            _undoableCommands = new List<Command>();
            _undoIndex = 0;
        }
    }
}