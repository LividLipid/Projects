namespace ConsoleMenu
{
    public class CommandNull : Command
    {
        public CommandNull(Handler receiver) : base(receiver)
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
            return "";
        }
    }
}