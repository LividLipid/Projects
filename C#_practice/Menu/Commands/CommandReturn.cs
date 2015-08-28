using System;
using MenuSystem;

namespace Commands
{
    [Serializable]
    public class CommandReturn : Command
    {
        public CommandReturn(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteReturnCommand();
        }
    }
}