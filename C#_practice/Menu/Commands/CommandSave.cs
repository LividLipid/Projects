using System;
using MenuSystem;

namespace Commands
{
    [Serializable]
    public class CommandSave : Command
    {
        public CommandSave(MenuHandler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteSaveCommand();
        }
    }
}