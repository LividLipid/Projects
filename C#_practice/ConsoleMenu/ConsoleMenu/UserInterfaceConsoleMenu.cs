using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMenu
{
    public class UserInterfaceConsoleMenu
    {
        private Handler _handler;
        private List<string> _entries = new List<string>();
        private List<Command> _commands = new List<Command>();
        private int _subItemCount;
        private string _menuTitle;
        private int _blankLineNr;

        private int _cursorPosition = 0;
        private int _firstEntryNumber = 1;

        private Command _chosenCommand;

        public UserInterfaceConsoleMenu(Handler handler, DataMenu data)
        {
            _handler = handler;
            _menuTitle = data.MenuTitle;

            var titles = data.ChildrenTitles;
            
            BuildEntriesAndCommands(titles);
            _blankLineNr = titles.Count;
            _subItemCount = titles.Count;
        }

        private void BuildEntriesAndCommands(List<string> titles)
        {
            for (var i = 0; i < titles.Count; i++)
            {
                _entries.Add(titles[i]);
                _commands.Add(new CommandSelect(_handler, i));
            }

            _entries.Add("Save");
            _commands.Add(new CommandSave(_handler));
            _entries.Add("Return");
            _commands.Add(new CommandReturn(_handler));
            _entries.Add("Quit");
            _commands.Add(new CommandQuit(_handler));
        }

        public void Display_Menu()
        {
            do
            {
                PrintMenuText();

                ConsoleKeyInfo cki = Console.ReadKey(true);
                bool keyIsDigit = char.IsDigit(cki.KeyChar);
                if (keyIsDigit)
                    ProcessDigitInput(cki.KeyChar);
                else
                    ProcessNonDigitInput(cki.Key);

                LoopCursorPosition();
            } while (_chosenCommand == null);

            _chosenCommand.AddToCommandQueue();
        }

        private void PrintMenuText()
        {
            var menuText = BuildMenuText();

            Console.Clear();
            Console.WriteLine(_menuTitle);
            Console.WriteLine();

            for (int i = 0; i < menuText.Count; i++)
            {
                if (i == _cursorPosition)
                    WriteSelectedLine(menuText[i]);
                else
                    WriteNormalLine(menuText[i]);
            }
        }

        private List<string> BuildMenuText()
        {
            var menuText = new List<string>();
            for (int i = 0; i < _entries.Count; i++)
            {
                int entryNr = i + _firstEntryNumber;
                menuText.Add("[" + entryNr + "] " + _entries[i]);
            }

            menuText.Insert(_blankLineNr, "");

            return menuText;
        }

        private void WriteNormalLine(string line)
        {
            Console.WriteLine(line);
        }

        private void WriteSelectedLine(string line)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(line);
            Console.ResetColor();
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
            if (_cursorPosition == _blankLineNr)
                _cursorPosition--;
        }

        private void IncrementCursorPosition()
        {
            _cursorPosition++;
            if (_cursorPosition == _blankLineNr)
                _cursorPosition++;
        }

        private void LoopCursorPosition()
        {
            if (_cursorPosition < 0)
                _cursorPosition = _entries.Count;
            if (_cursorPosition > _entries.Count)
                _cursorPosition = 0;
        }

        private void ChooseCommand(int entrySelection)
        {
            if (entrySelection >= _blankLineNr)
                entrySelection--;
            if (IsSelectionWithinBounds(entrySelection))
                _chosenCommand = _commands[entrySelection];
        }

        private bool IsSelectionWithinBounds(int entrySelection)
        {
            return (entrySelection >= 0) && (entrySelection <= _entries.Count);
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
    }
}