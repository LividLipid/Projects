using System;
using MenuSystem;

namespace Commands
{
    public class CommandCreate : Command
    {
        public string ItemTitle;
        public int CreatableTypeIndex;

        public CommandCreate(MenuHandler receiver, int creatableTypeIndex, string title) : base(receiver)
        {
            ItemTitle = title;
            CreatableTypeIndex = creatableTypeIndex;
        }

        public override void Execute()
        {
            if(CreatableTypeIndex < 0)
                throw new Exception("Type of new item not selected.");
            if (string.IsNullOrEmpty(ItemTitle))
                throw new Exception("Title of new item not selected.");
            Receiver.ExecuteCreateCommand(CreatableTypeIndex, ItemTitle);
        }
    }
}