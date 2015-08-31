using System;
using MenuControlBoundary;

namespace Commands
{

    public class SelectCommand : Command
    {

        private readonly int _childSelectedIndex;

        public SelectCommand(IMenuControlInterface receiver, int childSelected) : base(receiver)
        {
            _childSelectedIndex = childSelected;
        }

        public override void Execute()
        {
            if (_childSelectedIndex >= 0)
                Receiver.Select(_childSelectedIndex);
            else
                throw new Exception("No child selected.");
        }
    }
}