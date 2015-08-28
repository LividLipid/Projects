using System;
using Menu;

namespace Commands
{
    public class CommandNewItemAdd : Command
    {
        public string ItemTitle;
        public Type TypeOfItem;
        public Item AddedItem;

        public CommandNewItemAdd(Handler receiver, Type type, string title) : base(receiver)
        {
            TypeOfItem = type;
            ItemTitle = title;
        }

        public override void Execute()
        {
            if(TypeOfItem == null)
                throw new Exception("Type of new item not set.");
            if (ItemTitle == null)
                throw new Exception("Title of new item not set.");
            Receiver.ExecuteAddNewItemCommand(TypeOfItem, ItemTitle);
        }
    }
}