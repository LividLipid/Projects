namespace ConsoleMenu
{
    public class CommandRemoveItem : Command
    {
        public CommandRemoveItem(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void UnExecute()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUndoable()
        {
            throw new System.NotImplementedException();
        }

        public override string GetDefaultText()
        {
            return "Remove item";
        }
    }
}