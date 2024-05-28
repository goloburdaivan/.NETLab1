using Lab5.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Decorators
{
    public class AssembledSystemUnit : ProductDecorator
    {
        private List<Product> _components;

        public AssembledSystemUnit(List<Product> components) : base(null)
        {
            _components = components;
        }

        public override decimal GetPrice()
        {
            decimal totalPrice = 0;
            foreach (var component in _components)
            {
                totalPrice += component.GetPrice();
            }
            return totalPrice * 1.15m;
        }

        public override string GetDescription()
        {
            string description = "Assembled System Unit:\n";
            foreach (var component in _components)
            {
                description += $"  - {component.GetDescription()}\n";
            }
            return description + "(15% markup included)";
        }
    }
}
