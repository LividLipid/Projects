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

        //private static readonly List<string> ConfirmAlwaysOperations = new List<string>()
        //{
        //    Quit,
        //};

        //private static readonly List<string> ConfirmIfUnsavedOperations = new List<string>()
        //{
        //    Select,
        //    Return,
        //};

        //private static readonly List<string> TextSpecifiedOperations = new List<string>()
        //{
        //    Create,
        //};

        //private static readonly List<string> UndoableOperations = new List<string>()
        //{
        //    Create,
        //    Delete
        //};


        //public static bool ConfirmableAlways(string op)
        //{
        //    return ConfirmAlwaysOperations.Contains(op);
        //}

        //public static bool ConfirmableWhenUnsaved(string op)
        //{
        //    return ConfirmIfUnsavedOperations.Contains(op);
        //}

        //public static bool CheckIfTextSpecified(string op)
        //{
        //    return TextSpecifiedOperations.Contains(op); 
        //}

        //public static bool CheckIfUndoable(string op)
        //{
        //    return UndoableOperations.Contains(op);
        //}
    }
}