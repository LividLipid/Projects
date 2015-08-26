using System.Collections.Generic;

namespace ConsoleMenu
{
    public class UndoRedo : IUndoRedo
    {
        public Stack<ChangeRepresentation> UndoableChanges = new Stack<ChangeRepresentation>();
        public Stack<ChangeRepresentation> RedoableChanges = new Stack<ChangeRepresentation>();

        public void Undo(int level)
        {
            if (UndoableChanges.Count == 0) return;
            var change = UndoableChanges.Pop();
            RedoableChanges.Push(change);
            switch (change.Action)
            {
                case ActionType.Add:
                    break;
                case ActionType.Remove:
                    break;
                case ActionType.Rename:
                    break;
                case ActionType.MoveUp:
                    break;
                case ActionType.MoveDown:
                    break;
                default:
                    break;
            }
        }

        public void Redo(int level)
        {
            throw new System.NotImplementedException();
        }

        public void InsertObjectforUndoRedo(ChangeRepresentation dataobject)
        {
            throw new System.NotImplementedException();
        }
    }
}