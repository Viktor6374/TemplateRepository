using System;

namespace Shops.Exceptions
{
    public class ProductException : Exception
    {
        private ProductException(string massage)
            : base(massage) { }
        public static ProductException InvalidName(string name)
        {
            return new ProductException($"Invalid Name: {name}");
        }

        public static ProductException NullValue()
        {
            return new ProductException($"ProductIsNullValue");
        }

        public static ProductException MergingOfDifferentProductsException(Product product1, Product product2)
        {
            return new ProductException($"Еhe products must be the same: {product1}, {product2}");
        }
    }
}
