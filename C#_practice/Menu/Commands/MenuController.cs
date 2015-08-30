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
            Command cmd = new CommandQuit(_receiver);
            cmd.Execute();
        }

        public void Return()
        {
            Command cmd = new CommandReturn(_receiver);
            cmd = MakeMemoryResetting(cmd);
            cmd.Execute();
        }

        public void Save()
        {
            Command cmd = new CommandSave(_receiver);
            cmd.Execute();
        }

        public void Undo()
        {
            Command cmd = new CommandUndo(_receiver);
            cmd.Execute();
        }

        public void Redo()
        {
            Command cmd = new CommandRedo(_receiver);
            cmd.Execute();
        }

        public void Select(int selection)
        {
            Command cmd = new CommandSelect(_receiver, selection);
            cmd = MakeMemoryResetting(cmd);
            cmd.Execute();
        }

        public void ShowPossibleNewItems()
        {
            Command cmd = new CommandNewItemSelect(_receiver);
            cmd.Execute();
        }

        public void Create(int creatableTypeIndex, string title)
        {
            Command cmd = new CommandCreate(_receiver, creatableTypeIndex, title);
            cmd = MakeUndoable(cmd);
            cmd.Execute();
        }

        public void Delete(int selection)
        {
            Command cmd = new CommandDelete(_receiver, selection);
            cmd = MakeUndoable(cmd);
            cmd.Execute();
        }

        private Command MakeUndoable(Command cmd)
        {
            return new UndoableDecorator(_receiver, cmd);
        }

        private Command MakeMemoryResetting(Command cmd)
        {
            return new MemoryResetDecorator(_receiver, cmd);
        }
    }
}