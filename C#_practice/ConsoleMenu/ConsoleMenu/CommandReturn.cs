namespace ConsoleMenu
{
    public class CommandReturn : Command
    {
        public CommandReturn(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteReturnCommand();
        }

        public override void UnExecute()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUndoable()
        {
            return false;
        }
    }
}