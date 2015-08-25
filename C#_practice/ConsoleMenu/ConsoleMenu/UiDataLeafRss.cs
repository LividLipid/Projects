namespace ConsoleMenu
{
    public class UIDataLeafRSS : UIDataLeaf
    {
        public string Address;

        public UIDataLeafRSS(string title, string address) : base(title)
        {
            Address = address;
        }
    }
}