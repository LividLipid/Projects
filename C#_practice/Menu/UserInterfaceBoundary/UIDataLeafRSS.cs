namespace UserInterfaceBoundary
{
    public class UIDataLeafRSS : UIDataLeaf
    {
        public string Address;

        public UIDataLeafRSS(string title, bool isRoot, string address) : base(title, isRoot)
        {
            Address = address;
        }
    }
}