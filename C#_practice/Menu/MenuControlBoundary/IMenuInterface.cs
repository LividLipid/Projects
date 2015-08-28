using System;

namespace MenuControlBoundary
{
    public interface IMenuInterface
    {
        void ExecuteRefreshCommand();
        void ExecuteQuitCommand();
        void ExecuteReturnCommand();
        void ExecuteSelectNewItemCommand();
        void ExecuteSelectCommand(int selection);
        void ExecuteAddNewItemCommand(Type type, string title);
        void ExecuteRemoveItemCommand(int selection);
        void ExecuteSaveCommand();
        void AddUndoableState();
        void ExecuteUndoCommand();
    }
}
