using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MicroCompany
{
    internal class MissingOportunity : IEnumerable<string>
    {
        public MissingOportunity()
        {
            loss = new Dictionary<string, int>();
        }

        internal void RegisterLoss(string itemTag, int amount)
        {
            if (!loss.ContainsKey(itemTag))
                loss.Add(itemTag, 0);
            loss[itemTag] += amount;
        }

        private readonly Dictionary<string, int> loss;

        public int this[string tag]
        {
            get { return loss[tag]; }
        }
        public IEnumerator<string> GetEnumerator()
        {
            return loss.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return loss.Keys.GetEnumerator();
        }


        public int IncomeLoss => loss.Values.Sum();
    }
}