namespace ConsoleMenu
{
    public class ItemLeafRSS : ItemLeaf
    {
        public string Address;

        public ItemLeafRSS(string title) : base(title)
        {
        }

        public override UIData GetDataStructure()
        {
            return new UIDataLeafRSS(Title, Address);
        }

        public override string GetItemTypeName()
        {
            return "RSS Feed";
        }
    }
}