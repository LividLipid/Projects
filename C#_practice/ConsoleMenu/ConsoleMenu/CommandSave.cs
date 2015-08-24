using System;

namespace ConsoleMenu
{
    [Serializable]
    public class CommandSave : Command
    {
        public CommandSave(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteSaveCommand();
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