using System;
using MenuSystem;

namespace Commands
{
    [Serializable]
    public class CommandSelect : Command
    {

        private readonly int _childSelectedIndex;

        public CommandSelect(Handler receiver, int childSelected) : base(receiver)
        {
            _childSelectedIndex = childSelected;
        }

        public override void Execute()
        {
            if (_childSelectedIndex >= 0)
                Receiver.ExecuteSelectCommand(_childSelectedIndex);
            else
                throw new Exception("No child selected.");
        }
    }
}