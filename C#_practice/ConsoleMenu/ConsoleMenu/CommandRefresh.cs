namespace ConsoleMenu
{
    public class CommandRefresh : Command
    {
        public CommandRefresh(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteRefreshCommand();
        }

        public override string GetDefaultText()
        {
            return "Refresh";
        }
    }
}