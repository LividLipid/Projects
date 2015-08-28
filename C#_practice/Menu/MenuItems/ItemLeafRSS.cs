namespace MenuItems
{
    public class ItemLeafRSS : ItemLeaf
    {
        public string Address;

        public ItemLeafRSS(string title) : base(title)
        {
        }

        public override string GetItemTypeName()
        {
            return "RSS Feed";
        }
    }
}