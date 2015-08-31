using System;
using MenuControlBoundary;

namespace Commands
{

    public class ReturnCommand : Command
    {
        public ReturnCommand(IMenuControlInterface receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.Return();
        }
    }
}