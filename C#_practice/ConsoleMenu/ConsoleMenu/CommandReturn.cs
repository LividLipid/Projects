using System;

namespace ConsoleMenu
{
    [Serializable]
    public class CommandReturn : Command
    {
        public CommandReturn(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteReturnCommand();
        }

        public override string GetDefaultText()
        {
            return "Return";
        }
    }
}