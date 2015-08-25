using System;

namespace ConsoleMenu
{
    [Serializable]
    public abstract class Command
    {
        protected Handler Receiver;

        protected Command(Handler receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
        public abstract void UnExecute();
        
        public void AddToCommandQueue()
        {
            Receiver.CommandsToExecute.Enqueue(this);
        }

        public void RememberCommand()
        {
            if (IsUndoable())
                Receiver.ExecutedCommands.Add(this);
        }

        public abstract bool IsUndoable();
        public abstract string GetDefaultText();
    }
}