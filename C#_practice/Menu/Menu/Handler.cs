using System;
using System.Collections.Generic;
using System.Linq;
using UserInterfaceBoundary;

namespace Menu
{
    [Serializable]
    public abstract class Handler : MenuInterface
    {
        public static string DefaultFolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";
        private Item _treeRoot;
        private Item _currentItem;
        public string _treeName;
        public UserInterface _ui = new UserInterfaceConsole();
        public Saver _saver = new SaverBinarySerializer();
        public string _folderPath = DefaultFolderPath;

        private Stack<Item> _undoableStates = new Stack<Item>();
        private Stack<Item> _redoableStates = new Stack<Item>();
        //private Stack<Item> _addedItems = new Stack<Item>();
        //private Stack<Item> _removedItems = new Stack<Item>();

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
        }

        public override void ExecuteRefreshCommand()
        {
            ShowItem(_currentItem);
        }

        public override void ExecuteQuitCommand()
        {
            Environment.Exit(0);
        }

        public override void ExecuteReturnCommand()
        {
            if (!_currentItem.IsRoot())
                ShowItem(_currentItem.Parent);
            else
                ExecuteQuitCommand();
        }

        public override void ExecuteSelectNewItemCommand()
        {
            var creatableTypes = Item.GetCreatableItemTypes();
            var data = new UIDataNewItem("Add new item", creatableTypes);

            ShowData(data);
        }

        public override void ExecuteSelectCommand(int selection)
        {
            var selectedItem = _currentItem.GetChild(selection);

            ShowItem(selectedItem);
        }

        public override void ExecuteAddNewItemCommand(Type type, string title)
        {
            var itemToAdd = ItemFactory.Create(type, title);
            _currentItem.AddChild(itemToAdd);
            ShowItem(_currentItem);
        }

        public override void ExecuteRemoveItemCommand(int selection)
        {
            _currentItem.RemoveChild(selection);
            ShowItem(_currentItem);
        }

        public override void ExecuteSaveCommand()
        {
            
        }

        public override void AddUndoableState()
        {
            var currentState = ObjectCopier.Clone(_currentItem);
            _undoableStates.Push(currentState);
        }

        public override void ExecuteUndoCommand()
        {
            if (_undoableStates.Count > 0)
            {
                var previousState = _undoableStates.Pop();
                _redoableStates.Push(previousState);
                _currentItem = previousState;
            }
            ShowItem(_currentItem);
        }
    }
}