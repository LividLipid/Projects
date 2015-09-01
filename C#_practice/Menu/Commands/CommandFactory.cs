using MenuControlBoundary;

namespace Commands
{
    public class CommandFactory
    {
        private IMenuControlInterface _receiver;

        public CommandFactory()
        {
            
        }

        public CommandFactory(IMenuControlInterface receiver)
        {
            SetReceiver(receiver);
        }

        public void SetReceiver(IMenuControlInterface receiver)
        {
            _receiver = receiver;
        }

        public Command GetQuitCommand()
        {
            Command cmd = new QuitCommand(_receiver);
            return cmd;
        }

        public Command GetReturnCommand()
        {
            Command cmd = new ReturnCommand(_receiver);
            return cmd;
        }

        public Command GetSaveCommand()
        {
            Command cmd = new SaveCommand(_receiver);
            return cmd;
        }

        public Command GetSelectCommand(int selection)
        {
            Command cmd = new SelectCommand(_receiver, selection);
            return cmd;
        }

        public Command GetCreateCommand(int creatableTypeIndex, string title, string textData)
        {
            Command cmd = new CreateCommand(_receiver, creatableTypeIndex, title, textData);
            return cmd;
        }

        public Command GetDeleteCommand(int selection)
        {
            Command cmd = new DeleteCommand(_receiver, selection);
            return cmd;
        }

        public Command GetNullCommand()
        {
            Command cmd = new NullCommand(_receiver);
            return cmd;
        }
    }
}