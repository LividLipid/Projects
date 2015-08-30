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
        private Item _currentItem; 
        private string _name;
        private IUserInterface _ui;
        private Saver _saver;
        private string _folderPath = DefaultFolderPath;

        private Item _savedState; // Clone of Item before unsaved changes.
        private Stack<Item> _undoableStates;
        private Stack<Item> _redoableStates;

        public MenuHandler(string name)
        {
            _name = name;
            ResetMemory();
        }

        public MenuHandler(string name, IUserInterface ui, Saver saver)
        {
            _name = name;
            _ui = ui;
            _saver = saver;
            ResetMemory();
        }

        public void ResetMemory()
        {
            _undoableStates = new Stack<Item>();
            _redoableStates = new Stack<Item>();
        }

        public void SetUserInterface(IUserInterface ui)
        {
            _ui = ui;
        }

        public void SetSaver(Saver saver)
        {
            _saver = saver;
        }

        public void DisplayMenu(Item item)
        {
            DisplayNewItem(item.GetRoot());
        }

        private void DisplayNewItem(Item item)
        {
            _currentItem = item;
            _savedState = CopyCurrentState();
            var data = UIDataFactory.CreateUIData(_currentItem);
            DisplayDataObject(data);
        }

        private void RefreshDisplay()
        {
            var data = UIDataFactory.CreateUIData(_currentItem);
            DisplayDataObject(data);
        }

        private void DisplayDataObject(UIData data)
        {
            _ui.DisplayUserInterface(data);
        }

        private string GetFilePath()
        {
            return _folderPath + @"\" + _name;
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

        public void ExecuteQuitCommand()
        {
            Environment.Exit(0);
        }

        public void ExecuteReturnCommand()
        {
            if (!_currentItem.IsRoot())
                DisplayNewItem(_currentItem.Parent);
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

            DisplayNewItem(selectedItem);
        }

        public void ExecuteCreateCommand(int creatableTypeIndex, string title)
        {
            var creatableTypes = Item.GetCreatableItemTypes();
            var type = creatableTypes[creatableTypeIndex];
            var itemToAdd = ItemFactory.Create(type, title);
            _currentItem.AddChild(itemToAdd);
            RefreshDisplay();
        }

        public void ExecuteDeleteCommand(int selection)
        {
            _currentItem.RemoveChild(selection);
            RefreshDisplay();
        }

        public void ExecuteSaveCommand()
        {
            _savedState = _currentItem;
            RefreshDisplay();
        }

        public void AddUndoableState()
        {
            _undoableStates.Push(CopyCurrentState());

            if (_redoableStates.Count > 0)
                _redoableStates.Clear();
        }

        public void ExecuteUndoCommand()
        {
            if (_undoableStates.Count > 0)
            {
                _redoableStates.Push(CopyCurrentState());
                _currentItem = _undoableStates.Pop();
            }
            RefreshDisplay();
        }

        public void ExecuteRedoCommand()
        {
            if (_redoableStates.Count > 0)
            {
                _undoableStates.Push(CopyCurrentState());
                _currentItem = _redoableStates.Pop();
            }
            RefreshDisplay();
        }

        private Item CopyCurrentState()
        {
            return ObjectCopier.Clone(_currentItem);
        }
    }
}