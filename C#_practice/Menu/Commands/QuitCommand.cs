using System;
using MenuControlBoundary;

namespace Commands
{

    public class QuitCommand : Command
    {
        public QuitCommand(IMenuControlInterface receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.Quit();
        }
    }
}