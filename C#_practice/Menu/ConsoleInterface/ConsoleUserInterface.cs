using System;
using System.Collections.Generic;
using Commands;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        private readonly CommandFactory _commandFactory;

        public ConsoleUserInterface(CommandFactory cmdFactory)
        {
            _commandFactory = cmdFactory;
        }

        public void DisplayUserInterface(UIData data)
        {
            var type = data.GetType();
            Queue<Command> commands;
            ConsoleScreen screen;

            // Choose the type of console screen to display based on the type of the data object.
            if (type == typeof (UIDataMenu))
            {
                screen = new MainMenuScreen((UIDataMenu) data, _commandFactory);
                commands = screen.DisplayScreenAndReturnCommands();
            }
                
            else if (type == typeof (UIDataLeaf))
            {
                screen = new LeafScreen((UIDataLeaf) data, _commandFactory);
                commands = screen.DisplayScreenAndReturnCommands();
            }
            else
                throw new ArgumentException("Unknown data object type.");

            while (commands.Count > 0)
            {
                commands.Dequeue().Execute();
            }
        }
    }
}