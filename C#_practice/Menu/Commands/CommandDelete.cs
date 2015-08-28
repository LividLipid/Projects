using System;
using Menu;

namespace Commands
{
    public class CommandDelete : Command
    {
        private readonly int _childSelectedIndex;

        public CommandDelete(Handler receiver, int childSelected) : base(receiver)
        {
            _childSelectedIndex = childSelected;
        }

        public override void Execute()
        {
            if (_childSelectedIndex >= 0)
                Receiver.ExecuteDeleteCommand(_childSelectedIndex);
            else
                throw new Exception("No child selected.");
        }
    }
}