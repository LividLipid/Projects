using System;
using System.Runtime.CompilerServices;

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
        
        
        public void AddToCommandQueue()
        {
            Receiver.CommandsToExecute.Enqueue(this);
        }

        public void RememberCommand()
        {
            if (IsUndoable())
                Receiver.UndoableCommands.Add(this);
        }

        public abstract string GetDefaultText();

        public bool IsUndoable()
        {
            return (this is Undoable);
        }

        public bool RequiresTextSpecification()
        {
            return (this is CommandTextSpecified);
        }

        public bool RequiresConfirmation()
        {
            return (this is Confirmable);
        }
    }
}