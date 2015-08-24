using System;

namespace ConsoleMenu
{
    [Serializable]
    public class CommandSelect : Command
    {

        private readonly int _childSelectedIndex = -1;

        public CommandSelect(Handler receiver) : base(receiver)
        {
        }

        public CommandSelect(Handler receiver, int childSelected) : base(receiver)
        {
            _childSelectedIndex = childSelected;
        }

        public override void Execute()
        {
            if (_childSelectedIndex >= 0)
                Receiver.ExecuteSelectCommand(_childSelectedIndex);
            else
                throw new Exception("Select Command exception: no child selected.");
        }

        public override void UnExecute()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUndoable()
        {
            return false;
        }
    }
}