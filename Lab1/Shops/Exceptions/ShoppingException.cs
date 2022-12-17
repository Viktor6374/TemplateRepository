using System;

namespace Shops.Exceptions
{
    public class ShoppingException : Exception
    {
        private ShoppingException(string massage)
            : base(massage) { }
        public static ShoppingException NoProductInStock(int thereAreProducts, int productsNeeded)
        {
            return new ShoppingException($"There are products: {thereAreProducts}, products needed: {productsNeeded}");
        }

        public static ShoppingException InvalidQuantity(int quantity)
        {
            return new ShoppingException($"Invalid quantityt: {quantity}");
        }

        public static ShoppingException NullArgument()
        {
            return new ShoppingException($"Argument == Null");
        }
    }
}
