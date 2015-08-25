using System;

namespace ConsoleMenu
{
    [Serializable]
    public abstract class Command
    {
        protected Handler Receiver;
        public string TextSpcecification;

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

        public virtual bool RequiresTextSpecification()
        {
            return false;
        }

        public void SetTextSpecification(string text)
        {
            TextSpcecification = text;
        }
    }
}