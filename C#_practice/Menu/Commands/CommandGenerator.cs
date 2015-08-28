using System;
using Menu;
using MenuControlBoundary;

namespace Commands
{
    public class CommandGenerator : IMenuInterface
    {
        private Handler _receiver;

        public CommandGenerator(Handler receiver)
        {
            _receiver = receiver;
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public void Return()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void SelectItem(int selection)
        {
            throw new NotImplementedException();
        }

        public void ShowPossibleNewItems()
        {
            throw new NotImplementedException();
        }

        public void AddNewItem(Type type, string title)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int selection)
        {
            throw new NotImplementedException();
        }
    }
}