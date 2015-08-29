using MenuSystem;

namespace Commands
{
    public class CommandUndo : Command
    {
        public CommandUndo(MenuHandler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteUndoCommand();
        }
    }
}