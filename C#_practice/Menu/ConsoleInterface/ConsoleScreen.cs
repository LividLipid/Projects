using System;
using System.Collections.Generic;
using System.Linq;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public abstract class ConsoleScreen
    {
        protected ConsoleUserInterface UserInterface;
        protected string Title;
        protected int FirstEntryNumber;

        // Entries and operations, which must be initialied by subclasses.
        // Lists with element for each selectable console line.
        protected List<string> Entries; // Lines of selectable text.
        protected List<string> EntryOperations; // Text coded operations corresponding to entries.
        protected List<bool> DeletableEntries; // True if entry can be deleted.
        // This list maps entries to indices of selectable data. -1 if entry has no data.
        protected List<int> EntryDataIndices;

        protected int CursorPosition;
        protected string OperationToExecute;

        // Optional inputs. Some operations require:
        public string OptionalInputText; // Additional text (i.e. new item title).
        public int OptionalInputIndex; // Index of selected entry among data (i.e. nth child).
        
        protected ConsoleScreen(UIData data, ConsoleUserInterface ui)
        {
            CursorPosition = -1;
            InitConsoleScreen(data, ui);
        }

        protected ConsoleScreen(UIData data, ConsoleUserInterface ui, int cursorPosition)
        {
            CursorPosition = cursorPosition;
            InitConsoleScreen(data, ui);
        }

        private void InitConsoleScreen(UIData data, ConsoleUserInterface ui)
        {
            Title = data.Title;
            UserInterface = ui;
            FirstEntryNumber = 1;
            Entries = new List<string>();
            EntryOperations = new List<string>();
            DeletableEntries = new List<bool>();
            EntryDataIndices = new List<int>();
            OperationToExecute = Operations.Null;

            ResetOptionalInput();
            ArrangeEntriesAndOperations(data);
            SetCursorPosition();
        }

        private void ResetOptionalInput()
        {
            OptionalInputText = "";
            OptionalInputIndex = -1;
        }

        private void SetCursorPosition()
        {
            // If the cursor position is not already set,
            // this sets it to the first menu entry that is not blank.
            if (CursorPosition >= 0) return;
            var i = 0;
            while (EntryOperations[i] == Operations.Null)
                i++;
            CursorPosition = i;
        }

        public string DisplayScreenAndReturnCommand()
        {
            do
            {
                UpdateMenu();

            } while (OperationToExecute == Operations.Null);

            return OperationToExecute;
        }

        protected abstract void ArrangeEntriesAndOperations(UIData data);

        protected void UpdateMenu()
        {
            PrintMenuText();
            ReadKey();
        }

        protected abstract void PrintMenuText();

        protected void ReadKey()
        {
            var cki = Console.ReadKey(true);
            var keyIsDigit = char.IsDigit(cki.KeyChar);
            if (keyIsDigit)
                ProcessDigitInput(cki.KeyChar);
            else
                ProcessNonDigitInput(cki);
        }

        protected void ProcessDigitInput(char keyChar)
        {
            var digitMenuIndexed = (int)char.GetNumericValue(keyChar);
            var digit = digitMenuIndexed - FirstEntryNumber;
            ProcessSelectedIndex(digit);
        }

        protected abstract void ProcessNonDigitInput(ConsoleKeyInfo cki);

        protected void ProcessSelectedIndex(int i)
        {
            if (!IndexIsValid(i)) return;
            ProcessSelectedOperation(EntryOperations[i]);
        }

        protected void ProcessSelectedOperation(string operation)
        {
            GetOptionalInput(operation);

            var readyToExecute = true;
            if (ConfirmationIsNeeded(operation))
                readyToExecute = GetConfirmation(operation);

            if (readyToExecute)
                SetOperationToExecute(operation);
            else
                ResetOptionalInput();
        }

        private void GetOptionalInput(string operation)
        {
            var requiresText = Operations.CheckIfTextSpecified(operation);
            if(requiresText)
                OptionalInputText = GetTextInput(operation);

            OptionalInputIndex = EntryDataIndices[CursorPosition];
        }

        protected string GetTextInput(string operation)
        {
            string request;
            switch (operation)
            {
                case Operations.Create:
                    request = "Please enter title of new item and confirm.";
                    break;
                default:
                    request = "";
                    break;
            }
            var text = RequestTextInput(request);
            return text;
        }

        protected string RequestTextInput(string request)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(request);
            Console.WriteLine();
            Console.ResetColor();
            return Console.ReadLine();
        }

        protected bool ConfirmationIsNeeded(string operation)
        {
            var doConfirm = false;
            if (UserInterface.ChangesAreUnsaved())
                if (Operations.ConfirmableWhenUnsaved(operation))
                    doConfirm = true;

            if (Operations.ConfirmableAlways(operation))
                doConfirm = true;

            return doConfirm;
        }

        protected bool GetConfirmation(string operation)
        {
            string request;
            switch (operation)
            {
                case Operations.Quit:
                    request = "Really quit? (Unsaved changes will be lost.)";
                    break;
                case Operations.Return:
                    request = "Return to previous item? (Unsaved changes will be lost.)";
                    break;
                case Operations.Select:
                    request = "Go to next item? (Unsaved changes will be lost.)";
                    break;
                default:
                    request = "";
                    break;
            }
            return RequestConfirmation(request);
        }

        protected bool RequestConfirmation(string request)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(request);
            Console.WriteLine("Press Y to confirm or N to cancel.");
            Console.WriteLine();
            Console.ResetColor();

            while (true)
            {
                var cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                }
            }
        }

        private void SetOperationToExecute(string operation)
        {
            OperationToExecute = operation;
        }

        protected void WriteHighlightedLine(string line)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLine(line);
            Console.ResetColor();
        }

        protected void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        protected void LoopCursorPosition()
        {
            if (CursorPosition < 0)
                CursorPosition = Entries.Count - 1;
            if (CursorPosition > Entries.Count - 1)
                CursorPosition = 0;
        }

        protected void DecrementtCursorPosition()
        {
            CursorPosition--;
            LoopCursorPosition();
            var lineIsBlank = EntryOperations[CursorPosition] == Operations.Null;
            if (lineIsBlank)
                CursorPosition--;
        }

        protected void IncrementCursorPosition()
        {
            CursorPosition++;
            LoopCursorPosition();
            var lineIsBlank = EntryOperations[CursorPosition] == Operations.Null;
            if (lineIsBlank)
                CursorPosition++;
        }

        protected bool IndexIsValid(int i)
        {
            return IsSelectionWithinBounds(i);
        }

        protected bool IsSelectionWithinBounds(int entrySelection)
        {
            return (entrySelection >= 0) && (entrySelection <= Entries.Count);
        }

        protected virtual void ProcessUpArrowKey()
        {
            DecrementtCursorPosition();
        }
        protected virtual void ProcessDownArrowKey()
        {
            IncrementCursorPosition();
        }

        protected virtual void ProcessEnterKey()
        {
            ProcessSelectedIndex(CursorPosition);
        }

        protected virtual void ProcessEscapeKey()
        {
            ProcessSelectedOperation(Operations.Quit);
        }
        protected virtual void ProcessBackspaceKey()
        {
            ProcessSelectedOperation(Operations.Return);
        }
        
        protected virtual void ProcessDeleteKey()
        {
            if (!IndexIsValid(CursorPosition)) return;
            var selectionIsDeletable = DeletableEntries[CursorPosition] == false;
            if (!selectionIsDeletable) return;
            ProcessSelectedOperation(Operations.Delete);
        }

        protected virtual void ProcessUndoKey()
        {
            ProcessSelectedOperation(Operations.Undo);
        }

        protected virtual void ProcessRedoKey()
        {
            ProcessSelectedOperation(Operations.Redo);
        }
    }
}