using System;
using System.Security.Claims;
using MenuSystem;
using MenuControlBoundary;

namespace Commands
{
    public class MenuController : IMenuControlInterface
    {
        private MenuHandler _receiver;

        public MenuController()
        {
            
        }

        public MenuController(MenuHandler receiver)
        {
            SetReceiver(receiver);
        }

        public void SetReceiver(MenuHandler receiver)
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

        public void Select(int selection)
        {
            new CommandSelect(_receiver, selection).Execute();
        }

        public void ShowPossibleNewItems()
        {
            new CommandNewItemSelect(_receiver).Execute();
        }

        public void Create(int creatableTypeIndex, string title)
        {
            var cmd = new CommandCreate(_receiver, creatableTypeIndex, title);
            new UndoableDecorator(_receiver, cmd).Execute();
        }

        public void Delete(int selection)
        {
            var cmd = new CommandDelete(_receiver, selection);
            new UndoableDecorator(_receiver, cmd).Execute();
        }
    }
}