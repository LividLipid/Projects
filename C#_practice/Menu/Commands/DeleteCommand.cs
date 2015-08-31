using System;
using MenuControlBoundary;

namespace Commands
{
    public class DeleteCommand : Command
    {
        private readonly int _childSelectedIndex;

        public DeleteCommand(IMenuControlInterface receiver, int childSelected) : base(receiver)
        {
            _childSelectedIndex = childSelected;
        }

        public override void Execute()
        {
            if (_childSelectedIndex >= 0)
                Receiver.Delete(_childSelectedIndex);
            else
                throw new Exception("No child selected.");
        }
    }
}