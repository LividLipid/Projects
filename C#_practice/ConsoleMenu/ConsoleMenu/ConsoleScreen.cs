namespace ConsoleMenu
{
    public abstract class ConsoleScreen
    {
        protected Handler ItemHandler;
        protected UIData ItemData;
        protected string Title;

        protected Command ChosenCommand;

        public abstract void Display();

        protected ConsoleScreen(Handler handler, UIData data)
        {
            ItemHandler = handler;
            ItemData = data;
            Title = data.Title;
        }
    }
}