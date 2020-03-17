using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MicroCompany
{

    internal class Item
    {
        public Item(string name, string tag, int amount)
        {
            Name = name;
            Tag = tag;
            Amount = amount;
        }

        public string Name { get; }
        public string Tag { get; }
        public int Amount { get; }

        public override string ToString()
        {
            return $"{Tag}[{Amount}]";
        }
    }
    internal class Stock : IEnumerable<Item>
    {
        private readonly Dictionary<string, Item> stock;
        public int Capacity { get; }
        public Stock(int capacity, params Item[] items)
        {
            Capacity = capacity;
            if (items.Select(x => x.Amount).Sum() > capacity)
                throw new MaximumCapacityExceeded();
            stock = items.ToDictionary(x => x.Tag, x => x);
        }
        internal void Sell(Buyer buyer)
        {
            var item = stock[buyer.ItemTag];
            stock[buyer.ItemTag] = new Item(item.Name, item.Tag, item.Amount - buyer.Amount);
        }
        public Item this[string tag]
        {
            get { return stock[tag]; }
        }
        public IEnumerator<Item> GetEnumerator()
        {
            return stock.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return stock.Values.GetEnumerator();
        }
        internal bool CanSatisfy(Buyer buyer)
        {
            return stock[buyer.ItemTag].Amount >= buyer.Amount;
        }
        override public string ToString()
        {
            return $"Stock [{Capacity}]: {string.Join(' ', stock.Values)}";
        }
    }
}