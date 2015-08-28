using Menu;

namespace Commands
{
    public class CommandUndo : Command
    {
        public CommandUndo(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteUndoCommand();
        }
    }
}