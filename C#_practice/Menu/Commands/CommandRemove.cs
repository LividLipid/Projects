using System;
using Menu;

namespace Commands
{
    public class CommandRemove : Command, Undoable
    {
        private readonly int _childSelectedIndex = -1;

        public CommandRemove(Handler receiver) : base(receiver)
        {
        }

        public CommandRemove(Handler receiver, int childIndex) : base(receiver)
        {
            _childSelectedIndex = childIndex;
        }

        public override void Execute()
        {
            if (_childSelectedIndex >= 0)
            {
                Receiver.AddUndoableState();
                Receiver.ExecuteRemoveItemCommand(_childSelectedIndex);
            }
            else
                throw new Exception("Select Command exception: no child selected.");
        }

        public void AddUndoableState()
        {
            Receiver.AddUndoableState();
        }

        public override string GetDefaultText()
        {
            return "Remove item";
        }
    }
}