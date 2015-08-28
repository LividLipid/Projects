using System;

namespace MenuControlBoundary
{
    public interface IMenuInterface
    {
        void Quit();
        void Return();
        void Save();
        void Undo();
        void Redo();
        void SelectItem(int selection);
        void ShowPossibleNewItems();
        void AddNewItem(Type type, string title);
        void DeleteItem(int selection);
    }
}
