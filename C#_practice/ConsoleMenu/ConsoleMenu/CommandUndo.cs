namespace ConsoleMenu
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

        public override string GetDefaultText()
        {
            return "Undo";
        }
    }
}