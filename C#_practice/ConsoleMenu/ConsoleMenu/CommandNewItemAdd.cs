using System;

namespace ConsoleMenu
{
    public class CommandNewItemAdd : CommandTextSpecified, Undoable
    {
        public Type TypeOfItem;
        public Item AddedItem;

        public CommandNewItemAdd(Handler receiver) : base(receiver)
        {
        }

        public CommandNewItemAdd(Handler receiver, Type type) : base(receiver)
        {
            TypeOfItem = type;
        }

        public override void Execute()
        {
            if(TypeOfItem == null)
                throw new Exception("Type of new item not set.");
            if (TextSpcecification == null)
                throw new Exception("Title of new item not set.");
            Receiver.AddUndoableState();
            Receiver.ExecuteAddNewItemCommand(TypeOfItem, TextSpcecification);
        }

        public void AddUndoableState()
        {
            Receiver.AddUndoableState();
        }

        public override string GetDefaultText()
        {
            return "Add new";
        }

        public override string GetTextSpecificationRequest()
        {
            return "Please enter title and confirm.";
        }
    }
}