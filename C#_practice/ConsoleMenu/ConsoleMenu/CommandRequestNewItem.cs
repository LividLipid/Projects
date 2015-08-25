namespace ConsoleMenu
{
    public class CommandRequestNewItem : Command
    {
        public CommandRequestNewItem(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteSelectNewItemCommand();
        }

        public override void UnExecute()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUndoable()
        {
            return false;
        }

        public override string GetDefaultText()
        {
            return "New item";
        }
    }
}