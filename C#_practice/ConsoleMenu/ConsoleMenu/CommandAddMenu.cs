namespace ConsoleMenu
{
    public class CommandAddMenu : Command
    {
        public CommandAddMenu(Handler receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void UnExecute()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUndoable()
        {
            throw new System.NotImplementedException();
        }

        public override string GetDefaultText()
        {
            return "Add new menu";
        }
    }
}