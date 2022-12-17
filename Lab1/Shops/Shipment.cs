using Shops.Exceptions;

namespace Shops
{
    public class Shipment
    {
        public Shipment(Product product, decimal cost, int quantity)
        {
            if (product == null)
            {
                throw ProductException.NullValue();
            }

            if (cost <= 0)
            {
                throw ShipmentException.InvalidCost(cost);
            }

            if (quantity <= 0)
            {
                throw ShipmentException.InvalidQuantity(quantity);
            }

            Cost = cost;
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; private set; }
        public decimal Cost { get; private set; } = 0;
        public int Quantity { get; private set; } = 0;

        public void MergeShipments(Shipment addingShipment)
        {
            if (addingShipment == null)
            {
                throw ShipmentException.NullArgument();
            }

            if (Product.Id != addingShipment.Product.Id)
            {
                throw ProductException.MergingOfDifferentProductsException(Product, addingShipment.Product);
            }

            Quantity += addingShipment.Quantity;
        }

        public void ReduceQuantity(int quantity)
        {
            if (Quantity < quantity || quantity <= 0)
            {
                throw ShoppingException.NoProductInStock(Quantity, quantity);
            }

            Quantity -= quantity;
        }
    }
}
