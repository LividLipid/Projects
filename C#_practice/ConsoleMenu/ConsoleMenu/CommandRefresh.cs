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
            return "Refresh";
        }
    }
}