using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Products
{
    public class Motherboard : Product
    {
        public string FormFactor { get; set; }
        public string Chipset { get; set; }

        public override decimal GetPrice()
        {
            return Price;
        }

        public override string GetDescription()
        {
            return $"{Name} (SKU: {SKU}), {FormFactor} {Chipset} - ${Price}";
        }
    }
}
