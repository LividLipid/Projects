using MenuControlBoundary;

namespace Commands
{
    public class NullCommand : Command
    {
        public NullCommand(IMenuControlInterface receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}