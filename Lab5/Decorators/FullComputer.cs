using Lab5.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Decorators
{
    public class FullComputer : ProductDecorator
    {
        public FullComputer(Product product) : base(product) { }

        public override decimal GetPrice()
        {
            return _product.GetPrice() * 1.05m;
        }

        public override string GetDescription()
        {
            return $"{_product.GetDescription()} (Full Computer - 5% markup)";
        }
    }
}
