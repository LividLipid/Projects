using System;

namespace ConsoleMenu
{
    [Serializable]
    public class CommandSave : CommandConfirmable
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

        public override string GetDefaultText()
        {
            return "Save";
        }
    }
}