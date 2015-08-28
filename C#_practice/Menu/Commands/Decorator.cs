using MenuSystem;

namespace Commands
{
    public abstract class Decorator : Command
    {
        protected Command Command;

        protected Decorator(Handler receiver, Command cmd) : base(receiver)
        {
            Command = cmd;
        }

        public override void Execute()
        {
            Command.Execute();
        }
    }
}