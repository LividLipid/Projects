using MenuSystem;

namespace Commands
{
    public class CommandRedo : Command
    {
        public CommandRedo(MenuHandler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteRedoCommand();
        }
    }
}