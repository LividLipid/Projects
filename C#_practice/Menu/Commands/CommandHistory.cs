using System.Collections.Generic;
using Menu;

namespace Commands
{
    public class CommandHistory
    {
        private static CommandHistory _instance;
        protected List<Command> UndoableCommands = new List<Command>();
        protected int UndoIndex = -1;

        private CommandHistory() { }
        public static CommandHistory Instance => _instance ?? (_instance = new CommandHistory());

        public void ResetHistory()
        {
            UndoableCommands = new List<Command>();
            UndoIndex = -1;
        }

        public void AddCommand(Command cmd)
        {
            UndoableCommands.Add(cmd);
            UndoIndex++;
        }

        public bool HasUndoableCommand()
        {
            return UndoIndex >= 0;
        }

        public Command GetCommandToUndo()
        {
            var cmd = UndoableCommands[UndoIndex];
            UndoIndex--;
            return cmd;
        }
    }
}