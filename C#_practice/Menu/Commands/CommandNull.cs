using Menu;

namespace Commands
{
    public class CommandNull : Command
    {
        public CommandNull(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}