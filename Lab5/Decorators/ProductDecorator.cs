using Lab5.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Decorators
{
    public abstract class ProductDecorator : Product
    {
        protected Product _product;

        public ProductDecorator(Product product)
        {
            _product = product;
        }

        public override decimal GetPrice()
        {
            return _product.GetPrice();
        }

        public override string GetDescription()
        {
            return _product.GetDescription();
        }
    }
}
