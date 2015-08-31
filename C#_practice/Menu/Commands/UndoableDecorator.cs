

//using MenuControlBoundary;

//namespace Commands
//{
//    public class UndoableDecorator : Decorator
//    {

//        public UndoableDecorator(IMenuControlInterface receiver, Command cmd) : base(receiver, cmd)
//        {
//            cmd.IsUndoable = true;
//        }

//        public override void Execute()
//        {
//            Receiver.AddUndoableState();
//            Command.Execute();
//        }
//    }
//}