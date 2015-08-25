namespace ConsoleMenu
{
    public class ItemLeafRSS : ItemLeaf
    {
        public string Address;

        public ItemLeafRSS(string title) : base(title)
        {
        }

        public override Data GetDataStructure()
        {
            var itemData = new DataLeafRSS()
            {
                Title = Title,
                Address = Address
            };
            return itemData;
        }
    }
}