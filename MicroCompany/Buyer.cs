namespace MicroCompany
{
    internal class Buyer
    {
        public Buyer(int amount, string itemTag)
        {
            Amount = amount;
            ItemTag = itemTag;
        }

        public int Amount { get; }
        public string ItemTag { get; }

        public override string ToString()
        {
            return $"{ItemTag}[{Amount}]";
        }
    }
}