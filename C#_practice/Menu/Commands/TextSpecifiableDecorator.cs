using Menu;

namespace Commands
{
    public class TextSpecifiableDecorator : Decorator
    {
        public string TextSpecification;

        public TextSpecifiableDecorator(Handler receiver, Command cmd) : base(receiver, cmd)
        {
            cmd.IsTextspecifiable = true;
        }

        public void ExecuteWithText(string textInput)
        {
            TextSpecification = textInput;
            Execute();
        }
    }
}