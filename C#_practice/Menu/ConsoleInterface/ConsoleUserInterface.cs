using System;
using MenuControlBoundary;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        private readonly IMenuControlInterface _menu;
        private bool _hasUnsavedChanges;

        public ConsoleUserInterface(IMenuControlInterface menu)
        {
            _menu = menu;
            _hasUnsavedChanges = false;
        }

        public void DisplayUserInterface(UIData data)
        {
            var type = data.GetType();
            string operation;
            ConsoleScreen screen;

            // Choose the type of console screen to display based on the type of the data object.
            if (type == typeof (UIDataMenu))
            {
                screen = new ConsoleScreenMenuMain((UIDataMenu)data, this);
                operation = screen.DisplayScreenAndReturnCommand();
            }
            
            else if (type == typeof (UIDataNewTypes))
            {
                screen = new ConsoleScreenMenuAddNew((UIDataNewTypes) data, this);
                operation = screen.DisplayScreenAndReturnCommand();
            }
                

            else if (type == typeof (UIDataLeaf))
            {
                screen = new ConsoleScreenLeaf((UIDataLeaf) data, this);
                operation = screen.DisplayScreenAndReturnCommand();
            }
                
            else
                throw new ArgumentException("Unknown data object type.");

            var optionalIndex = screen.OptionalInputIndex;
            var optionalText = screen.OptionalInputText;
            switch (operation)
            {
                case Operations.Select:
                    _hasUnsavedChanges = false;
                    _menu.Save();
                    _menu.Select(optionalIndex);
                    break;
                case Operations.Create:
                    _hasUnsavedChanges = true;
                    _menu.Create(optionalIndex, optionalText);
                    break;
                case Operations.Delete:
                    _hasUnsavedChanges = true;
                    _menu.Delete(optionalIndex);
                    break;
                case Operations.Return:
                    _hasUnsavedChanges = false;
                    _menu.Save();
                    _menu.Return();
                    break;
                case Operations.Quit:
                    _menu.Quit();
                    break;
                case Operations.Save:
                    _hasUnsavedChanges = false;
                    _menu.Save();
                    break;
                case Operations.Undo:
                    _hasUnsavedChanges = true;
                    _menu.Undo();
                    break;
                case Operations.Redo:
                    _hasUnsavedChanges = true;
                    _menu.Redo();
                    break;
                case Operations.New:
                    _menu.ShowPossibleNewItems();
                    break;
                default:
                    throw new Exception("No operation selected.");
            }
        }

        public bool ChangesAreUnsaved()
        {
            return _hasUnsavedChanges;
        }
    }
}