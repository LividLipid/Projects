using System;
using System.Collections.Generic;
using MenuControlBoundary;
using MenuItems;
using UserInterfaceBoundary;

namespace MenuSystem
{
    [Serializable]
    public class MenuHandler : IMenuControlInterface
    {
        private const string DefaultFolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus";
        private Item _currentItem; 
        //private string _name;
        private IUserInterface _ui;
        //private Saver _saver;
        //private string _folderPath = DefaultFolderPath;

        public MenuHandler(string name)
        {
            //_name = name;
        }

        public MenuHandler(string name, IUserInterface ui, Saver saver)
        {
            //_name = name;
            _ui = ui;
            //_saver = saver;
        }

        public void SetUserInterface(IUserInterface ui)
        {
            _ui = ui;
        }

        public void SetSaver(Saver saver)
        {
            //_saver = saver;
        }

        public void DisplayMenu(Item item)
        {
            DisplayNewItem(item.GetRoot());
        }

        private void DisplayNewItem(Item item)
        {
            _currentItem = item;
            var data = UIDataFactory.CreateUIData(_currentItem);
            DisplayDataObject(data);
        }

        private void DisplayDataObject(UIData data)
        {
            _ui.DisplayUserInterface(data);
        }

        //private string GetFilePath()
        //{
        //    return _folderPath + @"\" + _name;
        //}

        //private void SaveHandler()
        //{
        //    if (_saver == null)
        //        throw new Exception("Saver has not been set.");
        //    _saver.SaveHandler(this, GetFilePath());
        //}

        public static MenuHandler LoadHandler(Saver saver, string filePath)
        {
            return saver.LoadHandler(filePath);
        }

        public void Quit()
        {
            Environment.Exit(0);
        }

        public void Return()
        {
            if (!_currentItem.IsRoot())
                DisplayNewItem(_currentItem.Parent);
            else
                Quit();
        }

        public void Select(int selection)
        {
            var selectedItem = _currentItem.GetChild(selection);

            DisplayNewItem(selectedItem);
        }

        public void Create(int creatableTypeIndex, string title, string textData)
        {
            var creatableTypes = Item.GetCreatableItemTypes();
            var type = creatableTypes[creatableTypeIndex];
            var itemToAdd = ItemFactory.Create(type, title, textData);
            _currentItem.AddChild(itemToAdd);
        }

        public void Delete(int selection)
        {
            _currentItem.RemoveChild(selection);
        }

        public void Save()
        {
            //_savedState = _currentItem;
            //RefreshDisplay();
        }
    }
}