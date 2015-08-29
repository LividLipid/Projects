using System;
using System.Collections.Generic;
using MenuItems;
using UserInterfaceBoundary;

namespace MenuSystem
{
    [Serializable]
    public class MenuHandler
    {
        private const string DefaultFolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";
        private Item _treeRoot;
        private Item _currentItem;
        private string _treeName;
        private IUserInterface _ui;
        private Saver _saver;
        private string _folderPath = DefaultFolderPath;

        private readonly Stack<Item> _undoableStates = new Stack<Item>();
        private readonly Stack<Item> _redoableStates = new Stack<Item>();

        public MenuHandler(string name, Item tree)
        {
            _treeName = name;
            _treeRoot = tree;
        }

        public MenuHandler(string name, Item tree, IUserInterface ui, Saver saver)
        {
            _treeName = name;
            _treeRoot = tree;
            _ui = ui;
            _saver = saver;
        }

        public void SetUserInterface(IUserInterface ui)
        {
            _ui = ui;
        }

        public void SetSaver(Saver saver)
        {
            _saver = saver;
        }

        public void DisplayMenu()
        {
            DisplayItem(_treeRoot);
        }

        private void DisplayItem(Item item)
        {
            _currentItem = item;
            var data = UIDataFactory.CreateUIData(item);
            DisplayDataObject(data);
        }

        private void DisplayDataObject(UIData data)
        {
            _ui.DisplayUserInterface(data);
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

        public static MenuHandler LoadHandler(Saver saver, string filePath)
        {
            return saver.LoadHandler(filePath);
        }

        public void ExecuteRefreshCommand()
        {
            DisplayItem(_currentItem);
        }

        public void ExecuteQuitCommand()
        {
            Environment.Exit(0);
        }

        public void ExecuteReturnCommand()
        {
            if (!_currentItem.IsRoot())
                DisplayItem(_currentItem.Parent);
            else
                ExecuteQuitCommand();
        }

        public void ExecuteSelectNewItemCommand()
        {
            var data = UIDataFactory.CreateNewTypesData();

            DisplayDataObject(data);
        }

        public void ExecuteSelectCommand(int selection)
        {
            var selectedItem = _currentItem.GetChild(selection);

            DisplayItem(selectedItem);
        }

        public void ExecuteCreateCommand(int creatableTypeIndex, string title)
        {
            var creatableTypes = Item.GetCreatableItemTypes();
            var type = creatableTypes[creatableTypeIndex];
            var itemToAdd = ItemFactory.Create(type, title);
            _currentItem.AddChild(itemToAdd);
            DisplayItem(_currentItem);
        }

        public void ExecuteDeleteCommand(int selection)
        {
            _currentItem.RemoveChild(selection);
            DisplayItem(_currentItem);
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
            DisplayItem(_currentItem);
        }

        public void ExecuteRedoCommand()
        {
            
        }
    }
}