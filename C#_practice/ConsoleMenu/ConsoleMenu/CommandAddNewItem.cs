using System;

namespace ConsoleMenu
{
    public class CommandAddNewItem : Command
    {
        public Type TypeOfItem;
        
        public CommandAddNewItem(Handler receiver) : base(receiver)
        {
        }

        public CommandAddNewItem(Handler receiver, Type type) : base(receiver)
        {
            TypeOfItem = type;
        }

        public override void Execute()
        {
            if(TypeOfItem == null)
                throw new Exception("Type of new item not set.");
            if (TextSpcecification == null)
                throw new Exception("Title of new item not set.");
            Receiver.ExecuteAddNewItemCommand(TypeOfItem, TextSpcecification);
        }

        public override void UnExecute()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUndoable()
        {
            return true;
        }

        public override string GetDefaultText()
        {
            return "Add new";
        }

        public override bool RequiresTextSpecification()
        {
            return true;
        }
    }
}