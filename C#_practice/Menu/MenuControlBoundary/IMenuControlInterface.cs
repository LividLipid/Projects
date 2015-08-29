using System;

namespace MenuControlBoundary
{
    public interface IMenuControlInterface
    {
        void Quit();
        void Return();
        void Save();
        void Undo();
        void Redo();
        void Select(int selection);
        void ShowPossibleNewItems();
        void Create(int creatableTypeIndex, string title);
        void Delete(int selection);
    }
}
