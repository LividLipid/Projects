using System;
using MenuControlBoundary;

namespace Commands
{

    public class SaveCommand : Command
    {
        public SaveCommand(IMenuControlInterface receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.Save();
        }
    }
}