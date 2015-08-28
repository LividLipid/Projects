using Menu;

namespace Commands
{
    public class CommandRedo : Command
    {
        public CommandRedo(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteRedoCommand();
        }
    }
}