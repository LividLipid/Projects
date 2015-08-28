using System;
using System.Collections.Generic;
using MenuItems;
using UserInterfaceBoundary;

namespace MenuSystem
{
    [Serializable]
    public class Handler
    {
        private const string DefaultFolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";
        private Item _treeRoot;
        private Item _currentItem;
        private string _treeName;
        private IUserInterface _ui;
        private Saver _saver = new SaverBinarySerializer();
        private string _folderPath = DefaultFolderPath;

        private readonly Stack<Item> _undoableStates = new Stack<Item>();
        private readonly Stack<Item> _redoableStates = new Stack<Item>();

        public Handler(string name, Item tree, IUserInterface ui, Saver saver)
        {
            _treeName = name;
            _treeRoot = tree;
            _ui = ui;
            _saver = saver;
        }

        public void ShowTree()
        {
            ShowItem(_treeRoot);
        }

        private void ShowItem(Item item)
        {
            _currentItem = item;
            var data = item.GetDataStructure();
            ShowData(data);
        }

        private void ShowData(UIData data)
        {
            _ui.Show(data);
        }

        private string GetFilePath()
        {
            return _folderPath + @"\" + _treeName;
        }

        private void SaveHandler()
        {
            if (_saver == null)
                throw new Exception("Saver has not been set.");
            _saver.SaveHandler(this, GetFilePath());
        }

        public static HandlerMenu LoadHandler(Saver saver, string filePath)
        {
            return saver.LoadHandler(filePath);
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
            var selectedItem = _currentItem.GetChild(selection);

            ShowItem(selectedItem);
        }

        public void ExecuteAddNewItemCommand(Type type, string title)
        {
            var itemToAdd = ItemFactory.Create(type, title);
            _currentItem.AddChild(itemToAdd);
            ShowItem(_currentItem);
        }

        public void ExecuteDeleteCommand(int selection)
        {
            _currentItem.RemoveChild(selection);
            ShowItem(_currentItem);
        }

        public void ExecuteSaveCommand()
        {
            
        }

        public void AddUndoableState()
        {
            var currentState = ObjectCopier.Clone(_currentItem);
            _undoableStates.Push(currentState);
        }

        public void ExecuteUndoCommand()
        {
            if (_undoableStates.Count > 0)
            {
                var previousState = _undoableStates.Pop();
                _redoableStates.Push(previousState);
                _currentItem = previousState;
            }
            ShowItem(_currentItem);
        }

        public void ExecuteRedoCommand()
        {
            
        }
    }
}