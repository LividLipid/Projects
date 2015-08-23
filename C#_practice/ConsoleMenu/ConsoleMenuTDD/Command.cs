namespace ConsoleMenuTDD
{
    public abstract class Command
    {
        protected Item Receiver;

        protected Command(Item receiver)
        {
            Receiver = receiver;
        }

        public abstract void Execute();
        public abstract void UnExecute();

    }
}