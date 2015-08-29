using System;
using MenuSystem;

namespace Commands
{
    [Serializable]
    public class CommandQuit : Command
    {
        public CommandQuit(MenuHandler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteQuitCommand();
        }
    }
}