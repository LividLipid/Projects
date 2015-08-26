namespace ConsoleMenu
{
    public abstract class CommandConfirmable : Command
    {
        protected CommandConfirmable(Handler receiver) : base(receiver)
        {
        }

        public override bool RequiresConfirmation()
        {
            return true;
        }
    }
}