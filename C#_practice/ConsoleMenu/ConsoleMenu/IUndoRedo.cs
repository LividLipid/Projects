namespace ConsoleMenu
{
    public interface IUndoRedo
    {
        void Undo(int level);
        void Redo(int level);
        void InsertObjectforUndoRedo(ChangeRepresentation dataobject);
    }
}