namespace ConsoleMenu
{
    public class CommandRemove : Command
    {
        public CommandRemove(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override string GetDefaultText()
        {
            return "Remove item";
        }
    }
}