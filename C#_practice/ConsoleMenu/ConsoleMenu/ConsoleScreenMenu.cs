using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public abstract class ConsoleScreenMenu : ConsoleScreen
    {
        protected readonly List<Command> MenuCommands = new List<Command>();
        protected readonly List<string> MenuText = new List<string>();
        protected readonly List<int> BlankLineNrs = new List<int>();
        protected bool MenuHasItems;

        protected int CursorPosition = 0;
        protected int FirstEntryNumber = 1;

        protected ConsoleScreenMenu(Handler handler, UIData data) : base(handler, data)
        {
        }

        protected abstract void BuildMenuDefaultSection();
        protected abstract void WriteInstructions(ConsoleColor color);
        protected abstract void ProcessNonDigitInput(ConsoleKey keyPress);

        protected void BuildMenu(List<string> entries)
        {
            if (MenuHasItems)
                BuildMenuEntriesSection(entries);
            BuildMenuDefaultSection();
        }

        protected void BuildMenuEntriesSection(List<string> entries)
        {
            for (var i = 0; i < entries.Count; i++)
            {
                AddSelectLine(new CommandSelect(Handler, i), entries[i]);
            }
            AddBlankLine();
        }

        protected void AddSelectLine(Command cmd, string lineText)
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
            MenuCommands.Add(new CommandNull(Handler));
            MenuText.Add("");
            BlankLineNrs.Add(MenuText.Count - 1);
        }

        public override void Display()
        {
            do
            {
                UpdateMenu();
            } while (ChosenCommand == null);

            ChosenCommand.AddToCommandQueue();
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
            ChooseCommand(digit);
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

        protected void ChooseCommand(int entrySelection)
        {
            if (IsSelectionWithinBounds(entrySelection))
                ChosenCommand = MenuCommands[entrySelection];
        }

        protected bool IsSelectionWithinBounds(int entrySelection)
        {
            return (entrySelection >= 0) && (entrySelection <= MenuCommands.Count);
        }

        protected void IssueReturnCommand()
        {
            ChosenCommand = new CommandReturn(Handler);
        }

        protected void IssueQuitCommand()
        {
            ChosenCommand = new CommandQuit(Handler);
        }

        private void IssueSaveCommand()
        {
            ChosenCommand = new CommandSave(Handler);
        }

        private void IssueRemoveItemCommand()
        {
            ChosenCommand = new CommandRemoveItem(Handler);
        }

        protected virtual void ProcessEnterKey()
        {
            ChooseCommand(CursorPosition);
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
            IssueRemoveItemCommand();
        }
        protected virtual void ProcessUpArrowKey()
        {
            DecrementtCursorPosition();
        }

        protected virtual void ProcessDownArrowKey()
        {
            IncrementCursorPosition();
        }
    }
}