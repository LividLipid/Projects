namespace ConsoleMenu
{
    public abstract class CommandTextSpecified : Command
    {
        public string TextSpcecification;

        protected CommandTextSpecified(Handler receiver) : base(receiver)
        {
        }

        public override bool RequiresTextSpecification()
        {
            return true;
        }

        public void SetTextSpecification(string text)
        {
            TextSpcecification = text;
        }

        public string GetTextSpecificationRequest()
        {
            return "Please enter title and confirm.";
        }
    }
}