using MenuSystem;

namespace Commands
{
    public class UndoableDecorator : Decorator
    {

        public UndoableDecorator(MenuHandler receiver, Command cmd) : base(receiver, cmd)
        {
            cmd.IsUndoable = true;
        }

        public override void Execute()
        {
            Receiver.AddUndoableState();
            Command.Execute();
        }
    }
}