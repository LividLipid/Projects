using System;

namespace ConsoleMenu
{
    public interface IMenuItem
    {
        string Title { get; set; }
        void Select();
    }
}