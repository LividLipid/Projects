using System;
using MenuSystem;

namespace Commands
{
    public abstract class Command
    {
        protected MenuHandler Receiver;
        public bool IsUndoable = false;

        protected Command(MenuHandler receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
    }
}