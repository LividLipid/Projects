using System;
using Menu;

namespace Commands
{
    [Serializable]
    public class CommandSave : Command, Confirmable
    {
        public CommandSave(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteSaveCommand();
        }

        public override string GetDefaultText()
        {
            return "Save";
        }
    }
}