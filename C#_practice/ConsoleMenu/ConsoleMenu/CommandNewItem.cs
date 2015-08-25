namespace ConsoleMenu
{
    public class CommandNewItem : Command
    {
        public CommandNewItem(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteNewItemCommand();
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
            return "New item";
        }
    }
}