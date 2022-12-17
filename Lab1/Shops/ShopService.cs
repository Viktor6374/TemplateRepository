using System.Collections.Generic;
using System.Linq;
using Shops.Exceptions;

namespace Shops
{
    public class ShopService
    {
        private List<Shop> shops = new List<Shop>();

        public IReadOnlyList<Shop> Shops => shops.AsReadOnly();
        public Shop AddShop(string address, string name)
        {
            var newShop = new Shop(address, name);
            shops.Add(newShop);

            return newShop;
        }

        public Product CreateProduct(string name)
        {
            var result = new Product(name);

            return result;
        }

        public void Purchasing(Buyer buyer, Purchase purchase, Shop shop)
        {
            if (purchase.ShoppingList == null)
            {
                throw DeliveryException.EmptyDelivery();
            }

            shop.Purchasing(buyer, purchase.ShoppingList);
        }

        public Shop? FindLowPrice(Product product, int quantity)
        {
            var minCost = shops.SelectMany(shop => shop.Assortment).Where(shipment => shipment.Product == product && shipment.Quantity >= quantity).Select(shipment => shipment.Cost).ToList();
            if (minCost.Count == 0)
            {
                return null;
            }

            return shops.Where(shop => shop.Assortment.Any(shipment => shipment.Cost == minCost.Min() && shipment.Product == product)).First();
        }

        public void AddToDelivery(Product product, Delivery delivery, decimal cost, int quantity)
        {
            if (product == null || delivery == null)
            {
                throw PurchaseException.NullArgument();
            }

            delivery.AddShipment(new Shipment(product, cost, quantity));
        }

        public void AddToPurchase(Purchase purchase, Product product, int quantity)
        {
            if (product == null || purchase == null)
            {
                throw PurchaseException.NullArgument();
            }

            purchase.AddShopping(new Shopping(product, quantity));
        }
    }
}
