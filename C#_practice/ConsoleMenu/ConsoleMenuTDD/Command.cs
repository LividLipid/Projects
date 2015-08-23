namespace ConsoleMenuTDD
{
    public abstract class Command
    {
        protected MenuItem Receiver;

        protected Command(MenuItem receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
        public abstract void UnExecute();

    }

    public class CommandReturn : Command
    {
        public CommandReturn(MenuItem receiver) : base(receiver)
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