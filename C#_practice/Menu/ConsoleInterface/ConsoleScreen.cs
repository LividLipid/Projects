using System;
using System.Collections.Generic;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public abstract class ConsoleScreen
    {
        // Text coded operations.
        protected const string OperationSelect = "Select item";
        protected const string OperationAddNew = "Add new item";
        protected const string OperationDelete = "Delete item";
        protected const string OperationReturn = "Return";
        protected const string OperationQuit = "Quit";
        protected const string OperationSave = "Save";
        protected const string OperationUndo = "Undo";
        protected const string OperationRedo = "Redo";
        protected const string OperationBlank = "";

        protected List<string> ConfirmAlwaysOperations = new List<string>()
        {
            OperationQuit,
        };

        protected List<string> ConfirmIfUnsavedOperations = new List<string>()
        {
            OperationSelect,
            OperationReturn,
        };

        protected List<string> TextSpecifiedOperations = new List<string>()
        {
            OperationAddNew,
        };

        protected ConsoleUserInterface UserInterface;
        protected string Title;
        protected int FirstEntryNumber;

        protected List<string> Entries; // Lines of selectable text.
        protected List<string> Operations; // Text coded operations corresponding to entries.
        protected List<bool> DeletableEntries; // True if entry can be deleted.

        protected int CursorPosition;
        protected string OperationToExecute;
        protected string TextInput; // Some operations requires additional text (i.e. new item title).
        
        protected ConsoleScreen(UIData data, ConsoleUserInterface ui)
        {
            InitConsoleScreen(data, ui);
            CursorPosition = 0;
        }

        protected ConsoleScreen(UIData data, ConsoleUserInterface ui, int cursorPosition)
        {
            InitConsoleScreen(data, ui);
            CursorPosition = cursorPosition;
        }

        private void InitConsoleScreen(UIData data, ConsoleUserInterface ui)
        {
            Title = data.Title;
            UserInterface = ui;
            FirstEntryNumber = 1;

            
        }
        
        public void Display_Screen()
        {
            ArrangeEntriesAndOperations();

            do
            {
                UpdateMenu();
            } while (OperationToExecute == null);

            //SelectedAction.Execute();
        }

        protected abstract void ArrangeEntriesAndOperations();

        protected void UpdateMenu()
        {
            PrintMenuText();
            ReadKey();
            LoopCursorPosition();
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
            ProcessSelectedOperation(Operations[i]);
        }

        protected void ProcessSelectedOperation(string operation)
        {
            var readyToExecute = GetOptionalInputAndConfirm(operation);

            if (readyToExecute)
                SetOperationToExecute(operation);
        }

        private void SetOperationToExecute(string operation)
        {
            OperationToExecute = operation;
        }

        private bool GetOptionalInputAndConfirm(string operation)
        {
            TextInput = TextInputIsNeeded(operation) ? GetTextInput(operation) : null;

            var readyToExecute = true;
            if (ConfirmationIsNeeded(operation))
                readyToExecute = GetConfirmation(operation);

            return readyToExecute;
        }

        protected bool ConfirmationIsNeeded(string entry)
        {
            var doConfirm = false;
            if (UserInterface.UnsavedChangesExist())
                if (ConfirmIfUnsavedOperations.Contains(entry))
                    doConfirm = true;

            if (ConfirmAlwaysOperations.Contains(entry))
                doConfirm = true;

            return doConfirm;
        }

        protected bool TextInputIsNeeded(string entry)
        {
            return TextSpecifiedOperations.Contains(entry);
        }

        protected string GetTextInput(string entry)
        {
            string request;
            switch (entry)
            {
                case OperationAddNew:
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

        protected bool GetConfirmation(string entry)
        {
            string request;
            switch (entry)
            {
                case OperationQuit:
                    request = "Really quit? (Unsaved changes will be lost.)";
                    break;
                case OperationReturn:
                    request = "Return to previous item? (Unsaved changes will be lost.)";
                    break;
                case OperationSelect:
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
            var lineIsBlank = Entries[CursorPosition] == OperationBlank;
            if (lineIsBlank)
                CursorPosition--;
        }

        protected void IncrementCursorPosition()
        {
            CursorPosition++;
            var lineIsBlank = Entries[CursorPosition] == OperationBlank;
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
            ProcessSelectedOperation(OperationQuit);
        }
        protected virtual void ProcessBackspaceKey()
        {
            ProcessSelectedOperation(OperationReturn);
        }
        
        protected virtual void ProcessDeleteKey()
        {
            if (!IndexIsValid(CursorPosition)) return;
            var selectionIsDeletable = DeletableEntries[CursorPosition] == false;
            if (!selectionIsDeletable) return;
            SetOperationToExecute(OperationDelete);
        }

        protected virtual void ProcessUndoKey()
        {
            SetOperationToExecute(OperationUndo);
        }

        protected virtual void ProcessRedoKey()
        {
            SetOperationToExecute(OperationRedo);
        }
    }
}