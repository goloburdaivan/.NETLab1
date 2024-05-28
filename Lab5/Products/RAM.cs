using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Products
{
    public class RAM : Product
    {
        public int Capacity { get; set; }
        public int Speed { get; set; }

        public override decimal GetPrice()
        {
            return Price;
        }

        public override string GetDescription()
        {
            return $"{Name} (SKU: {SKU}), {Capacity}GB {Speed}MHz - ${Price}";
        }
    }
}
