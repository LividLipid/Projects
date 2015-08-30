using MenuSystem;

namespace Commands
{
    public class MemoryResetDecorator : Decorator
    {
        public MemoryResetDecorator(MenuHandler receiver, Command cmd) : base(receiver, cmd)
        {
        }

        public override void Execute()
        {
            Receiver.ResetMemory();
            Command.Execute();
        }
    }
}