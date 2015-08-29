using MenuSystem;

namespace Commands
{
    public class CommandNewItemSelect : Command
    {
        public CommandNewItemSelect(MenuHandler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteSelectNewItemCommand();
        }
    }
}