using System;

namespace ConsoleMenu
{
    [Serializable]
    public class CommandQuit : Command
    {
        public CommandQuit(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteQuitCommand();
        }

        public override void UnExecute()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUndoable()
        {
            return false;
        }
    }
}