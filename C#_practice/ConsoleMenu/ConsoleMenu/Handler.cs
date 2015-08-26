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

        private Stack<Item> _addedItems = new Stack<Item>();
        private Stack<Item> _removedItems = new Stack<Item>();

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

            _addedItems.Push(itemToAdd);
            ShowItem(_currentItem);
        }

        public void UndoAddNewItemCommand()
        {
            var lastAddedItem = _addedItems.Pop();
            _currentItem.RemoveChild(lastAddedItem);

            _removedItems.Push(lastAddedItem);
            ShowItem(_currentItem);
        }

        public void ExecuteRemoveItemCommand(int selection)
        {
            var removedItem = _currentItem.GetChild(selection);
            _currentItem.RemoveChild(selection);

            _removedItems.Push(removedItem);
            ShowItem(_currentItem);
        }

        public void UndoRemoveItemCommand()
        {
            var lastRemovedItem = _removedItems.Pop();
            _currentItem.AddChild(lastRemovedItem);

            _addedItems.Push(lastRemovedItem);
            ShowItem(_currentItem);
        }

        public void ExecuteSaveCommand()
        {

        }
    }
}