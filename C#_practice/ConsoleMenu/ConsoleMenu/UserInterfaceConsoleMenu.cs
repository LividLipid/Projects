using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class UserInterfaceConsoleMenu
    {
        private Handler _handler;
        private string _menuTitle;
        private List<Command> _menuCommands = new List<Command>();
        private List<string> _menuText = new List<string>();
        private List<int> _blankLineNrs = new List<int>();
        private bool _menuHasItems;

        private int _cursorPosition = 0;
        private int _firstEntryNumber = 1;

        private Command _chosenCommand;

        public UserInterfaceConsoleMenu(Handler handler, DataMenu data)
        {
            _handler = handler;
            _menuTitle = data.MenuTitle;

            var titles = data.ChildrenTitles;
            _menuHasItems = titles.Count >= 1;

            if (_menuHasItems)
                BuildMenu(titles);
            else
                BuildEmptyMenu(titles);
        }

        private void BuildMenu(List<string> titles)
        {
            for (var i = 0; i < titles.Count; i++)
            {
                AddSelectLine(new CommandSelect(_handler, i), titles[i]);
            }
            AddBlankLine();
            AddDefaultLine(new CommandReturn(_handler));
            AddDefaultLine(new CommandNew(_handler));
            AddDefaultLine(new CommandSave(_handler));
            
            AddBlankLine();
            AddDefaultLine(new CommandQuit(_handler));
        }

        private void BuildEmptyMenu(List<string> titles)
        {
            AddDefaultLine(new CommandReturn(_handler));
            AddBlankLine();
            AddDefaultLine(new CommandQuit(_handler));
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
            _menuCommands.Add(new CommandNull(_handler));
            _menuText.Add("");
            _blankLineNrs.Add(_menuText.Count-1);
        }

        public void DisplayMenu()
        {
            do
            {
                UpdateMenu();
            } while (_chosenCommand == null);

            _chosenCommand.AddToCommandQueue();
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
            Console.WriteLine(_menuTitle);
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
                _chosenCommand = _menuCommands[entrySelection];
        }

        private bool IsSelectionWithinBounds(int entrySelection)
        {
            return (entrySelection >= 0) && (entrySelection <= _menuCommands.Count);
        }

        private void IssueReturnCommand()
        {
            _chosenCommand = new CommandReturn(_handler);
        }

        private void IssueQuitCommand()
        {
            _chosenCommand = new CommandQuit(_handler);
        }

        private void IssueSaveCommand()
        {
            _chosenCommand = new CommandSave(_handler);
        }

        private void IssueRemoveItemCommand()
        {
            _chosenCommand = new CommandRemoveItem(_handler);
        }
    }
}