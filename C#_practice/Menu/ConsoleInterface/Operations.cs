using System.Collections.Generic;

namespace ConsoleInterface
{
    public static class Operations
    {
        // Text coded operations.
        public const string Select = "Select item";
        public const string Create = "Create item";
        public const string Delete = "Delete item";
        public const string Return = "Return";
        public const string Quit = "Quit";
        public const string Save = "Save";
        public const string Undo = "Undo";
        public const string Redo = "Redo";
        public const string New = "Create new item";
        public const string Null = ""; // No operation selected.

        private static readonly List<string> ConfirmAlwaysCommands = new List<string>()
        {
            Quit,
        };

        private static readonly List<string> ConfirmIfUnsavedCommands = new List<string>()
        {
            Select,
            Return,
        };

        private static readonly List<string> TextSpecifiedCommands = new List<string>()
        {
            Create,
        };


        public static bool ConfirmableAlways(string cmd)
        {
            return ConfirmAlwaysCommands.Contains(cmd);
        }

        public static bool ConfirmableWhenUnsaved(string cmd)
        {
            return ConfirmIfUnsavedCommands.Contains(cmd);
        }

        public static bool CheckIfTextSpecified(string cmd)
        {
            return TextSpecifiedCommands.Contains(cmd); 
        }
    }
}