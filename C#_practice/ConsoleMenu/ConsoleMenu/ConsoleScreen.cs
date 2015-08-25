namespace ConsoleMenu
{
    public abstract class ConsoleScreen
    {
        protected Handler Handler;
        protected UIData ItemData;
        protected string Title;

        protected Command ChosenCommand;

        public abstract void Display();

        protected ConsoleScreen(Handler handler, UIData data)
        {
            Handler = handler;
            ItemData = data;
            Title = data.Title;
        }
    }
}