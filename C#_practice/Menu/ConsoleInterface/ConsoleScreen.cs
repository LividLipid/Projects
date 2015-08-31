using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Commands;
using UserInterfaceBoundary;

namespace ConsoleInterface
{
    public abstract class ConsoleScreen
    {
        protected CommandFactory CmdFactory;
        protected string Title;
        protected int FirstEntryNumber;

        protected List<MenuEntry> Entries; // Entries which must be initialied by subclasses.
        protected int CursorPosition;

        private Stack<Command> _undoableCommands;
        private Stack<Command> _redoableCommands;
        private Stack<MenuState> _previousStates;
        private Stack<MenuState> _subsequentStates;
        private Command _finalCommand;

        private UIData _inputData;

        protected ConsoleScreen(UIData data, CommandFactory cmdFactory)
        {
            InitConsoleScreen(data, cmdFactory);
        }

        private void InitConsoleScreen(UIData data, CommandFactory cmdFactory)
        {
            Title = data.Title;
            CmdFactory = cmdFactory;
            FirstEntryNumber = 1;
            Entries = new List<MenuEntry>();
            _undoableCommands = new Stack<Command>();
            _redoableCommands = new Stack<Command>();
            _previousStates = new Stack<MenuState>();
            _subsequentStates = new Stack<MenuState>();
            _inputData = data;
            CursorPosition = 0;

            ArrangeEntries(data);
        }

        protected abstract void ArrangeEntries(UIData data);

        public Queue<Command> DisplayScreenAndReturnCommands()
        {
            do
            {
                UpdateCursorPosition();
                PrintMenuText();
                ReadKeyAndProcess();

            } while (_finalCommand == null);

            return BuildCommandQueue();
        }

        private void UpdateCursorPosition()
        {
            LoopCursorPosition();
            if (Entries[CursorPosition].Operation != Operations.Null) return;
            if (CursorPosition == 0)
                IncrementCursorPosition();
            else
                DecrementtCursorPosition();
        }

        private Queue<Command> BuildCommandQueue()
        {
            // Convert the stack of undoable commands to a queue of commands to be executed by the system.
            var commandQueue = new Queue<Command>();
            while (_undoableCommands.Count > 0)
                commandQueue.Enqueue(_undoableCommands.Pop());
            commandQueue.Enqueue(_finalCommand);

            return commandQueue;
        }

        protected abstract void PrintMenuText();

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

        protected void ReadKeyAndProcess()
        {
            var cki = Console.ReadKey(true);
            var keyIsDigit = char.IsDigit(cki.KeyChar);
            if (keyIsDigit)
                ProcessDigitInput(cki.KeyChar);
            else
                ProcessNonDigitInput(cki);
        }

        protected abstract void ProcessNonDigitInput(ConsoleKeyInfo cki);

        protected void ProcessDigitInput(char keyChar)
        {
            var digitMenuIndexed = (int)char.GetNumericValue(keyChar);
            var digit = digitMenuIndexed - FirstEntryNumber;

            if (CheckIfOutOfBounds(digit)) return;

            ProcessSelectedEntry(Entries[digit]);
        }

        private bool CheckIfOutOfBounds(int i)
        {
            return (i < 0) || (i > Entries.Count);
        }

        protected int CountDataEntries()
        {
            return Entries.Count(entry => entry.Data != null);
        }

        private void ProcessSelectedEntry(MenuEntry entry)
        {
            switch (entry.Operation)
            {
                case Operations.Select:
                    Select(entry);
                    break;
                case Operations.Create:
                    Create(entry);
                    break;
                case Operations.Delete:
                    // Delete as a selectable entry is unimplemented. Using the hotkey works.
                    break;
                case Operations.Return:
                    Return();
                    break;
                case Operations.Quit:
                    Quit();
                    break;
                case Operations.Save:
                    Save();
                    break;
                case Operations.Undo:
                    Undo();
                    break;
                case Operations.Redo:
                    Redo();
                    break;
                case Operations.New:
                    OpenNewItemMenu();
                    break;
            }
        }

        protected virtual void ProcessUpArrowKey()
        {
            DecrementtCursorPosition();
        }
        protected virtual void ProcessDownArrowKey()
        {
            IncrementCursorPosition();
        }

        protected void DecrementtCursorPosition()
        {
            CursorPosition--;
            LoopCursorPosition();
            var lineIsBlank = Entries[CursorPosition].Operation == Operations.Null;
            if (lineIsBlank)
                CursorPosition--;
        }

        protected void IncrementCursorPosition()
        {
            CursorPosition++;
            LoopCursorPosition();
            var lineIsBlank = Entries[CursorPosition].Operation == Operations.Null;
            if (lineIsBlank)
                CursorPosition++;
        }

        protected void LoopCursorPosition()
        {
            if (CursorPosition < 0)
                CursorPosition = Entries.Count - 1;
            if (CursorPosition > Entries.Count - 1)
                CursorPosition = 0;
        }

        protected void ProcessEnterKey()
        {
            ProcessSelectedEntry(Entries[CursorPosition]);
        }

        protected void ProcessEscapeKey()
        {
            Quit();
        }

        protected void ProcessBackspaceKey()
        {
            Return();
        }

        protected void ProcessDeleteKey()
        {
            Delete(CursorPosition);
        }

        protected void ProcessUndoKey()
        {
            Undo();
        }

        protected void ProcessRedoKey()
        {
            Redo();
        }

        protected void Select(MenuEntry entry)
        {
            bool doProceed;
            bool doSave;
            const string request = "Save changes and proceed to selected item?";
            GetUnsavedChangesConfirmation(request, out doProceed, out doSave);
            
            if (doSave)
                Save();
            if (!doProceed) return;
            _finalCommand = CmdFactory.GetSelectCommand(entry.DataIndex);
        }

        protected void Create(MenuEntry entry)
        {
            const string request = "Please enter title of new item and confirm.";
            var name = GetTextInput(request);
            _finalCommand = CmdFactory.GetCreateCommand(entry.DataIndex, name);
        }

        protected void Delete(int index)
        {
            if (CheckIfOutOfBounds(index)) return;
            if (!Entries[index].IsDeletable) return;

            AddUndoableState();
            var dataIndex = Entries[index].DataIndex;
            var deleteCommand = CmdFactory.GetDeleteCommand(dataIndex);
            _undoableCommands.Push(deleteCommand);
            Entries.RemoveAt(index);
        }

        protected void Return()
        {
            bool doProceed;
            bool doSave;
            const string request = "Save changes and return to previous menu?";
            GetUnsavedChangesConfirmation(request, out doProceed, out doSave);

            if (doSave)
                Save();
            if (!doProceed) return;
            _finalCommand = CmdFactory.GetReturnCommand();
        }

        protected void Quit()
        {
            bool doProceed;
            bool doSave;
            const string request = "Save changes and quit?";
            GetUnsavedChangesConfirmation(request, out doProceed, out doSave);

            if (doSave)
                Save();
            if (!doProceed) return;
            _finalCommand = CmdFactory.GetQuitCommand();
        }

        protected void Save()
        {
            _finalCommand = CmdFactory.GetSaveCommand();
        }

        protected void Undo()
        {
            if (_undoableCommands.Count <= 0) return;
            _subsequentStates.Push(StoreCurrentState());
            _redoableCommands.Push(_undoableCommands.Pop());

            RestoreState(_previousStates.Pop());
        }

        protected void Redo()
        {
            if (_redoableCommands.Count <= 0) return;
            _previousStates.Push(StoreCurrentState());
            _undoableCommands.Push(_redoableCommands.Pop());

            RestoreState(_subsequentStates.Pop());
        }

        protected void OpenNewItemMenu()
        {
            // Get a create command from a new item menu screen:
            var newScreen = new CreateNewMenuScreen(_inputData, CmdFactory);
            var cmds = newScreen.DisplayScreenAndReturnCommands();
            var returnedCmd = cmds.Dequeue();

            if (returnedCmd.GetType() != typeof (CreateCommand)) return;
            AddUndoableState();

            // Create a new entry:
            var createCmd = (CreateCommand) returnedCmd;
            var dataIndex = CountDataEntries();
            var title = createCmd.ItemTitle;
            var data = title;
            var operation = Operations.Select;
            var isDeletable = true;
            var newEntry = new MenuEntry(title, data, operation, isDeletable, dataIndex);
            
            InsertDataEntry(newEntry);
            _undoableCommands.Push(createCmd);
        }

        private void InsertDataEntry(MenuEntry newEntry)
        {
            if (CountDataEntries() == 0)
                Entries.Insert(0, newEntry);
            else
            {
                //Find the last data entry and insert the new entry after it:
                int lastDataEntry = 0;
                for (int i = 0; i < Entries.Count; i++)
                {
                    if (Entries[i].Data != null)
                        lastDataEntry = i;
                }
                Entries.Insert(lastDataEntry + 1, newEntry);
            }
            
        }

        private bool UnsavedChangesExist()
        {
            return _undoableCommands.Count > 0;
        }

        private void GetUnsavedChangesConfirmation(string request, out bool doProceed, out bool doSave)
        {
            doProceed = true;
            doSave = false;
            if (!UnsavedChangesExist()) return;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(request);
            Console.WriteLine("Press Y to save and proceed, N to proceed without saving, or C to cancel.");
            Console.WriteLine();
            Console.ResetColor();

            var recognizedInput = false;
            while (recognizedInput == false)
            {
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Y:
                        doSave = true;
                        recognizedInput = true;
                        break;
                    case ConsoleKey.N:
                        recognizedInput = true;
                        break;
                    case ConsoleKey.C:
                        doProceed = false;
                        recognizedInput = true;
                        break;
                }
            }
        }

        protected string GetTextInput(string request)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(request);
            Console.WriteLine();
            Console.ResetColor();

            string line = null;
            while (string.IsNullOrEmpty(line))
                line = Console.ReadLine();
            return line;
        }

        private void AddUndoableState()
        {
            _previousStates.Push(StoreCurrentState());

            // Making an undoable change resets redoable states;
            if(_redoableCommands.Count > 0)
                _redoableCommands.Clear();
            if (_subsequentStates.Count > 0)
                _subsequentStates.Clear();
        }

        private MenuState StoreCurrentState()
        {
            var currentState = new MenuState(Entries);
            return ObjectCopier.Clone(currentState);
        }

        private void RestoreState(MenuState state)
        {
            Entries = state.Entries;
        }
    }
}