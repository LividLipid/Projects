using System;
using MenuSystem;

namespace Commands
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
    }
}