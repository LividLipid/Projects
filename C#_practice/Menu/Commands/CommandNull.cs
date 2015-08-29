using MenuSystem;

namespace Commands
{
    public class CommandNull : Command
    {
        public CommandNull(MenuHandler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}