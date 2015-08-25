using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class ConsoleScreenMenu : ConsoleScreen
    {
        private readonly List<Command> _menuCommands = new List<Command>();
        private readonly List<string> _menuText = new List<string>();
        private readonly List<int> _blankLineNrs = new List<int>();
        private readonly bool _menuHasItems;

        private int _cursorPosition = 0;
        private int _firstEntryNumber = 1;

        public ConsoleScreenMenu(Handler handler, DataMenu data) : base(handler, data)
        {
            var titles = data.ChildrenTitles;
            _menuHasItems = titles.Count >= 1;

            if (_menuHasItems)
                BuildMenuEntriesSection(titles);
            BuildMenuDefaultSection();
        }

        private void BuildMenuEntriesSection(List<string> titles)
        {
            for (var i = 0; i < titles.Count; i++)
            {
                AddSelectLine(new CommandSelect(Handler, i), titles[i]);
            }
            AddBlankLine();
        }

        private void BuildMenuDefaultSection()
        {
            AddDefaultLine(new CommandReturn(Handler));
            AddDefaultLine(new CommandNewItem(Handler));
            AddDefaultLine(new CommandSave(Handler));

            AddBlankLine();
            AddDefaultLine(new CommandQuit(Handler));
        }

        private void AddSelectLine(Command cmd, string lineText)
        {
            _menuCommands.Add(cmd);
            _menuText.Add(lineText);
        }

        private void AddDefaultLine(Command cmd)
        {
            _menuCommands.Add(cmd);
            _menuText.Add(cmd.GetDefaultText());
        }

        private void AddBlankLine()
        {
            _menuCommands.Add(new CommandNull(Handler));
            _menuText.Add("");
            _blankLineNrs.Add(_menuText.Count-1);
        }

        public override void Display()
        {
            do
            {
                UpdateMenu();
            } while (ChosenCommand == null);

            ChosenCommand.AddToCommandQueue();
        }

        public void UpdateMenu()
        {
            PrintMenuText();
            ReadKey();
            LoopCursorPosition();
        }

        private void ReadKey()
        {
            ConsoleKeyInfo cki = Console.ReadKey(true);
            bool keyIsDigit = char.IsDigit(cki.KeyChar);
            if (keyIsDigit)
                ProcessDigitInput(cki.KeyChar);
            else
                ProcessNonDigitInput(cki.Key);
        }

        private void ProcessDigitInput(char keyChar)
        {
            var digitMenuIndexed = (int)char.GetNumericValue(keyChar);
            var digit = digitMenuIndexed - _firstEntryNumber;
            ChooseCommand(digit);
        }

        private void ProcessNonDigitInput(ConsoleKey keyPress)
        {
            switch (keyPress)
            {
                case ConsoleKey.Enter:
                    ChooseCommand(_cursorPosition);
                    break;
                case ConsoleKey.Escape:
                    IssueReturnCommand();
                    break;
                case ConsoleKey.Backspace:
                    IssueReturnCommand();
                    break;
                case ConsoleKey.Delete:
                    IssueRemoveItemCommand();
                    break;
                case ConsoleKey.UpArrow:
                    DecrementtCursorPosition();
                    break;
                case ConsoleKey.DownArrow:
                    IncrementCursorPosition();
                    break;
            }
        }

        private void DecrementtCursorPosition()
        {
            _cursorPosition--;
            if (_blankLineNrs.Contains(_cursorPosition))
                _cursorPosition--;
        }

        private void IncrementCursorPosition()
        {
            _cursorPosition++;
            if (_blankLineNrs.Contains(_cursorPosition))
                _cursorPosition++;
        }

        private void PrintMenuText()
        {
            WriteTitleSection(ConsoleColor.Red);
            if (!_menuHasItems)
                WriteEmptyEntryMessage(ConsoleColor.Yellow);
            WriteEntrySection();
            WriteInstructions(ConsoleColor.Red);
        }

        private void WriteTitleSection(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.ResetColor();
        }

        private void WriteEmptyEntryMessage(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("This menu contains no items.");
            Console.WriteLine();
            Console.ResetColor();
        }

        private void WriteEntrySection()
        {
            for (int i = 0; i < _menuText.Count; i++)
            {
                if (i == _cursorPosition)
                    WriteHighlightedLine(_menuText[i]);
                else
                    WriteLine(_menuText[i]);
            }
        }

        private void WriteInstructions(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine("Select item with arrow keys and Enter.");
            Console.WriteLine("Press Escape or Backspace to return.");
            if (_menuHasItems)
                Console.WriteLine("Press Delete to delete item.");
            Console.WriteLine();
            Console.ResetColor();
        }

        private void WriteHighlightedLine(string line)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLine(line);
            Console.ResetColor();
        }

        private void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        private void LoopCursorPosition()
        {
            if (_cursorPosition < 0)
                _cursorPosition = _menuCommands.Count-1;
            if (_cursorPosition > _menuCommands.Count-1)
                _cursorPosition = 0;
        }

        private void ChooseCommand(int entrySelection)
        {
            if (IsSelectionWithinBounds(entrySelection))
                ChosenCommand = _menuCommands[entrySelection];
        }

        private bool IsSelectionWithinBounds(int entrySelection)
        {
            return (entrySelection >= 0) && (entrySelection <= _menuCommands.Count);
        }

        private void IssueReturnCommand()
        {
            ChosenCommand = new CommandReturn(Handler);
        }

        private void IssueQuitCommand()
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
    }
}