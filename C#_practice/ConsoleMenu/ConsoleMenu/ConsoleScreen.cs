using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public abstract class ConsoleScreen
    {
        protected Handler ItemHandler;
        protected string Title;
        protected string ActionChosen;

        protected Command ChosenCommand;
        protected CommandHistory History = CommandHistory.Instance;
        

        public abstract void Display();

        protected ConsoleScreen(Handler handler, UIData data)
        {
            ItemHandler = handler;
            Title = data.Title;
        }

        protected void ChooseToExecute(Command cmd)
        {
            ChosenCommand = cmd;
            ActionChosen = "Execute";
            if (ChosenCommand.IsUndoable())
                History.AddCommand(ChosenCommand);
        }

        public void ChooseToUndo()
        {
            if (!History.HasUndoableCommand()) return;
            ChosenCommand = History.GetCommandToUndo();
            ActionChosen = "Undo";
        }

        public void ChooseToRedo()
        {
            throw new NotImplementedException();
        }
    }
}