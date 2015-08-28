using Menu;

namespace Commands
{
    public abstract class CommandTextSpecified : Command
    {
        public string TextSpcecification;

        protected CommandTextSpecified(Handler receiver) : base(receiver)
        {
        }

        public void SetTextSpecification(string text)
        {
            TextSpcecification = text;
        }

        public abstract string GetTextSpecificationRequest();
    }
}