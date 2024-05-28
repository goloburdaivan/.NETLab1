using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Products
{
    public class Processor : Product
    {
        public float ClockSpeed { get; set; }
        public int CoreCount { get; set; }

        public override decimal GetPrice()
        {
            return Price;
        }

        public override string GetDescription()
        {
            return $"{Name} (SKU: {SKU}), {ClockSpeed} GHz, {CoreCount} cores - ${Price}";
        }
    }
}
