namespace ConsoleMenuTDD
{
    public class CommandReturn : Command
    {
        public CommandReturn(Item receiver) : base(receiver)
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
    }
}