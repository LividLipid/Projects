using System;

namespace ConsoleMenu
{
    [Serializable]
    public class CommandQuit : Command, Confirmable
    {
        public CommandQuit(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ExecuteQuitCommand();
        }

        public override string GetDefaultText()
        {
            return "Quit";
        }
    }
}