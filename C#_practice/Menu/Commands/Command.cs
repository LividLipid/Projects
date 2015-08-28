using System;
using MenuSystem;

namespace Commands
{
    public abstract class Command
    {
        protected Handler Receiver;
        public bool IsUndoable = false;

        protected Command(Handler receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
    }
}