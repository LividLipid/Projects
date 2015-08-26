namespace ConsoleMenu
{
    public abstract class ConsoleScreen
    {
        protected Handler ItemHandler;
        protected string Title;

        protected Command ChosenCommand;

        public abstract void Display();

        protected ConsoleScreen(Handler handler, UIData data)
        {
            ItemHandler = handler;
            Title = data.Title;
        }
    }
}