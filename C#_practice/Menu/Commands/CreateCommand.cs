using System;
using MenuControlBoundary;

namespace Commands
{
    public class CreateCommand : Command
    {
        public string ItemTitle;
        public int CreatableTypeIndex;
        public string TextData;

        public CreateCommand(IMenuControlInterface receiver, int creatableTypeIndex, string title) : base(receiver)
        {
            ItemTitle = title;
            CreatableTypeIndex = creatableTypeIndex;
        }

        public CreateCommand(IMenuControlInterface receiver, int creatableTypeIndex, string title, string textData) : base(receiver)
        {
            ItemTitle = title;
            CreatableTypeIndex = creatableTypeIndex;
            TextData = textData;
        }

        public override void Execute()
        {
            if(CreatableTypeIndex < 0)
                throw new Exception("Type of new item not selected.");
            if (string.IsNullOrEmpty(ItemTitle))
                throw new Exception("Title of new item not selected.");
            Receiver.Create(CreatableTypeIndex, ItemTitle, TextData);
        }
    }
}