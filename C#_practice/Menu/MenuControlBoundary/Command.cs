using System;


namespace MenuControlBoundary
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