using Shops.Exceptions;

namespace Shops
{
    public class Shopping
    {
        public Shopping(Product product, int quantity)
        {
            if (product == null)
            {
                throw ShoppingException.NullArgument();
            }

            if (quantity == 0)
            {
                throw ShoppingException.InvalidQuantity(quantity);
            }

            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Quantity { get; }
    }
}
