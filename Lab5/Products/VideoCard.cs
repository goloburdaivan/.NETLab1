using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Products
{
    public class VideoCard : Product
    {
        public int MemorySize { get; set; }
        public string MemoryType { get; set; }

        public override decimal GetPrice()
        {
            return Price;
        }

        public override string GetDescription()
        {
            return $"{Name} (SKU: {SKU}), {MemorySize}GB {MemoryType} - ${Price}";
        }
    }
}
