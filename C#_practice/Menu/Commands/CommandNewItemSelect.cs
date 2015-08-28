using Menu;

namespace Commands
{
    public class CommandNewItemSelect : Command
    {
        public CommandNewItemSelect(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteSelectNewItemCommand();
        }

        public override string GetDefaultText()
        {
            return "New item";
        }
    }
}