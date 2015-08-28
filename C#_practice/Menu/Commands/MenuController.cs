using System;
using System.Security.Claims;
using Menu;
using MenuControlBoundary;

namespace Commands
{
    public class MenuController : IMenuInterface
    {
        private readonly Handler _receiver;

        public MenuController(Handler receiver)
        {
            _receiver = receiver;
        }

        public void Quit()
        {
            new CommandQuit(_receiver).Execute();
        }

        public void Return()
        {
            new CommandReturn(_receiver).Execute();
        }

        public void Save()
        {
            new CommandSave(_receiver).Execute();
        }

        public void Undo()
        {
            new CommandUndo(_receiver).Execute();
        }

        public void Redo()
        {
            new CommandRedo(_receiver).Execute();
        }

        public void SelectItem(int selection)
        {
            new CommandSelect(_receiver, selection).Execute();
        }

        public void ShowPossibleNewItems()
        {
            new CommandNewItemSelect(_receiver).Execute();
        }

        public void AddNewItem(Type type, string title)
        {
            var cmd = new CommandNewItemAdd(_receiver, type, title);
            new UndoableDecorator(_receiver, cmd).Execute();
        }

        public void DeleteItem(int selection)
        {
            var cmd = new CommandDelete(_receiver, selection);
            new UndoableDecorator(_receiver, cmd).Execute();
        }
    }
}