namespace ConsoleMenu
{
    public class CommandUndo : Command
    {
        public CommandUndo(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override string GetDefaultText()
        {
            throw new System.NotImplementedException();
        }
    }
}