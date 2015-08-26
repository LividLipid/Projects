using System;
using System.CodeDom;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public abstract class ConsoleScreenMenu : ConsoleScreen
    {
        protected readonly List<Command> MenuCommands = new List<Command>();
        protected readonly List<string> MenuText = new List<string>();
        protected readonly List<int> BlankLineNrs = new List<int>();
        protected bool MenuHasItems;
        protected List<int> DeletableItems = new List<int>(); 

        protected int CursorPosition = 0;
        protected int FirstEntryNumber = 1;

        protected ConsoleScreenMenu(Handler handler, UIData data) : base(handler, data)
        {
        }

        protected abstract void BuildMenuEntriesSection();
        protected abstract void BuildMenuDefaultSection();
        protected abstract void WriteInstructions(ConsoleColor color);
        protected abstract void ProcessNonDigitInput(ConsoleKey keyPress);

        protected void BuildMenu(UIData data)
        {
            if (MenuHasItems)
                BuildMenuEntriesSection();
            BuildMenuDefaultSection();
        }

        protected void AddEntryLine(Command cmd, string lineText)
        {
            MenuCommands.Add(cmd);
            MenuText.Add(lineText);
        }

        protected void AddDefaultLine(Command cmd)
        {
            MenuCommands.Add(cmd);
            MenuText.Add(cmd.GetDefaultText());
        }

        protected void AddBlankLine()
        {
            MenuCommands.Add(new CommandNull(ItemHandler));
            MenuText.Add("");
            BlankLineNrs.Add(MenuText.Count - 1);
        }

        public override void Display()
        {
            do
            {
                UpdateMenu();
            } while (ChosenCommand == null);

            switch (ActionChosen)
            {
                case "Execute":
                    ChosenCommand.Execute();
                    break;
                case "Undo":
                    var cmd = (Undoable) ChosenCommand;
                    cmd.Unexecute();
                    break;
                default:
                    throw new Exception("Unknown action.");
            }
            
        }

        protected void UpdateMenu()
        {
            PrintMenuText();
            ReadKey();
            LoopCursorPosition();
        }

        protected void ReadKey()
        {
            ConsoleKeyInfo cki = Console.ReadKey(true);
            bool keyIsDigit = char.IsDigit(cki.KeyChar);
            if (keyIsDigit)
                ProcessDigitInput(cki.KeyChar);
            else
                ProcessNonDigitInput(cki.Key);
        }

        protected void ProcessDigitInput(char keyChar)
        {
            var digitMenuIndexed = (int)char.GetNumericValue(keyChar);
            var digit = digitMenuIndexed - FirstEntryNumber;
            ProcessSelection(digit);
        }

        protected void DecrementtCursorPosition()
        {
            CursorPosition--;
            if (BlankLineNrs.Contains(CursorPosition))
                CursorPosition--;
        }

        protected void IncrementCursorPosition()
        {
            CursorPosition++;
            if (BlankLineNrs.Contains(CursorPosition))
                CursorPosition++;
        }

        protected void PrintMenuText()
        {
            WriteTitleSection(ConsoleColor.Red);
            if (!MenuHasItems)
                WriteEmptyEntryMessage(ConsoleColor.Yellow);
            WriteEntrySection();
            WriteInstructions(ConsoleColor.Red);
        }

        protected void WriteTitleSection(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.ResetColor();
        }

        protected void WriteEmptyEntryMessage(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("This menu contains no items.");
            Console.WriteLine();
            Console.ResetColor();
        }

        protected void WriteEntrySection()
        {
            for (int i = 0; i < MenuText.Count; i++)
            {
                if (i == CursorPosition)
                    WriteHighlightedLine(MenuText[i]);
                else
                    WriteLine(MenuText[i]);
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
                CursorPosition = MenuCommands.Count - 1;
            if (CursorPosition > MenuCommands.Count - 1)
                CursorPosition = 0;
        }

        protected bool SelectionIsValid(int i)
        {
            return IsSelectionWithinBounds(i);
        }

        protected void RequestOptionalInputAndExecute(Command cmd)
        {
            if (cmd.RequiresTextSpecification())
                cmd = SpecifyCommandText((CommandTextSpecified)cmd);

            var readyToExecute = true;
            if (cmd.RequiresConfirmation())
                readyToExecute = RequestConfirmation();

            if (readyToExecute)
                ChooseToExecute(cmd);
        }

        protected bool IsSelectionWithinBounds(int entrySelection)
        {
            return (entrySelection >= 0) && (entrySelection <= MenuCommands.Count);
        }

        protected CommandTextSpecified SpecifyCommandText(CommandTextSpecified cmd)
        {

            var text = RequestTextInput(cmd);
            cmd.SetTextSpecification(text);
            return cmd;
        }

        protected string RequestTextInput(CommandTextSpecified cmd)
        {
            var request = cmd.GetTextSpecificationRequest();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(request);
            Console.WriteLine();
            Console.ResetColor();
            return Console.ReadLine();
        }

        protected bool RequestConfirmation()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
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
                    case ConsoleKey.Escape:
                        return false;
                }
            }
        }

        protected void IssueReturnCommand()
        {
            ChooseToExecute(new CommandReturn(ItemHandler));
        }

        protected void IssueQuitCommand()
        {
            ChooseToExecute(new CommandQuit(ItemHandler));
        }

        private void IssueSaveCommand()
        {
            ChooseToExecute(new CommandSave(ItemHandler));
        }

        private void IssueRemoveItemCommand(int i)
        {
            ChooseToExecute(new CommandRemove(ItemHandler, i));
        }

        protected virtual void ProcessEnterKey()
        {
            ProcessSelection(CursorPosition);
        }

        protected virtual void ProcessSelection(int i)
        {
            if (!SelectionIsValid(i)) return;
            var cmd = MenuCommands[i];
            RequestOptionalInputAndExecute(cmd);
        }

        protected virtual void ProcessEscapeKey()
        {
            IssueReturnCommand();
        }
        protected virtual void ProcessBackspaceKey()
        {
            IssueReturnCommand();
        }
        protected virtual void ProcessDeleteKey()
        {
            if (!SelectionIsValid(CursorPosition)) return;
            if (!SelectionIsDeletable(CursorPosition)) return;
            IssueRemoveItemCommand(CursorPosition);
        }
        protected virtual void ProcessUpArrowKey()
        {
            DecrementtCursorPosition();
        }

        protected virtual void ProcessDownArrowKey()
        {
            IncrementCursorPosition();
        }

        protected bool SelectionIsDeletable(int i)
        {
            return DeletableItems.Contains(i);
        }
    }
}