using System;
using Menu;

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