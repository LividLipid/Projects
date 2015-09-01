using System;

namespace MenuControlBoundary
{
    public interface IMenuControlInterface
    {
        void Quit();
        void Return();
        void Save();
        void Select(int selection);
        void Create(int creatableTypeIndex, string title, string textData);
        void Delete(int selection);
    }
}
