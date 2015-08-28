using System;
using Menu;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public abstract class ConsoleScreen
    {
        protected Handler ItemHandler;
        protected string Title;
        protected string ActionChosen;

        protected Command ChosenCommand;
        

        public abstract void Display();

        protected ConsoleScreen(Handler handler, UIData data)
        {
            ItemHandler = handler;
            Title = data.Title;
        }

        protected void ChooseToExecute(Command cmd)
        {
            ChosenCommand = cmd;
        }

        public void ChooseToUndo()
        {
            ChosenCommand = new CommandUndo(ItemHandler);
        }

        public void ChooseToRedo()
        {
            throw new NotImplementedException();
        }
    }
}