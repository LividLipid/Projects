using System;
using MenuControlBoundary;

namespace Commands
{
    public abstract class Command
    {
        protected IMenuControlInterface Receiver;
        public bool IsUndoable = false;

        protected Command(IMenuControlInterface receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
    }
}