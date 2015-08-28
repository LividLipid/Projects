using System;
using MenuControlBoundary;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        private readonly IMenuInterface _menu;
        private bool _hasUnsavedChanges;

        public ConsoleUserInterface(IMenuInterface menu)
        {
            _menu = menu;
            _hasUnsavedChanges = false;
        }

        public void Show(UIData data)
        {
            var type = data.GetType();

            // Choose the type of console screen to display based on the type of the data object.
            if(type == typeof(UIDataMenu))
                new ConsoleScreenMenuMain((UIDataMenu) data, this).Display_Screen();
            else if (type == typeof(UIDataNewItem))
                new ConsoleScreenMenuAddNew((UIDataNewItem)data, this).Display_Screen();
            else if (type == typeof(UIDataLeaf))
                new ConsoleScreenLeaf((UIDataLeaf)data, this).Display_Screen();
            else
                throw new ArgumentException("Unknown data object type.");
        }

        public bool UnsavedChangesExist()
        {
            return _hasUnsavedChanges;
        }

        public void Quit()
        {
            _hasUnsavedChanges = false;
            _menu.Quit();
        }
        public void Return()
        {
            _hasUnsavedChanges = false;
            _menu.Return();
        }
        public void Save()
        {
            _hasUnsavedChanges = false;
            _menu.Save();
        }
        public void Undo()
        {
            _hasUnsavedChanges = true;
            _menu.Undo();
        }
        public void Redo()
        {
            _hasUnsavedChanges = true;
            _menu.Redo();
        }
        public void SelectItem(int selection)
        {
            _hasUnsavedChanges = false;
            _menu.SelectItem(selection);
        }
        public void ShowPossibleNewItems()
        {
            _menu.ShowPossibleNewItems();
        }
        public void AddNewItem(Type type, string title)
        {
            _hasUnsavedChanges = true;
            _menu.AddNewItem(type, title);
        }
        public void RemoveItem(int selection)
        {
            _hasUnsavedChanges = true;
            _menu.DeleteItem(selection);
        }
    }
}