using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Exceptions;

namespace Shops
{
    public class Shop
    {
        private List<Shipment> assortment = new List<Shipment>();

        public Shop(string address, string name)
        {
            if (address == null || name == null)
            {
                throw ShopException.NullArgument();
            }

            Address = address;
            Name = name;
        }

        public decimal Money { get; private set; } = 0;
        public string Address { get; }
        public string Name { get; }
        public Guid Guid_ { get; } = Guid.NewGuid();

        public IReadOnlyList<Shipment> Assortment => assortment.AsReadOnly();
        public void SetShipment(Delivery delivery)
        {
            foreach (Shipment shipment in delivery.Shipments)
            {
                Shipment? shipmentInAssortment = assortment.Where(shipment_ => shipment_.Product.Id == shipment.Product.Id).SingleOrDefault();

                if (shipmentInAssortment == null)
                {
                    assortment.Add(shipment);
                }
                else
                {
                    shipmentInAssortment.MergeShipments(shipment);
                }
            }
        }

        public void Purchasing(Buyer buyer, IReadOnlyList<Shopping> shoppings)
        {
            if (buyer == null || shoppings == null)
            {
                throw ShopException.NullArgument();
            }

            decimal amountOfMoney = 0;

            foreach (Shopping shopping in shoppings)
            {
                Shipment? shipment = assortment.Where(shipment_ => shopping.Product.Id == shipment_.Product.Id).SingleOrDefault();

                if (shipment == null)
                {
                    throw ShoppingException.NoProductInStock(0, shopping.Quantity);
                }

                if (shipment.Quantity < shopping.Quantity)
                {
                    throw ShoppingException.NoProductInStock(shipment.Quantity, shopping.Quantity);
                }

                amountOfMoney += shopping.Quantity * shipment.Cost;
            }

            if (buyer.Money < amountOfMoney)
            {
                throw BuyerException.NotEnoughMoney(buyer.Money, amountOfMoney);
            }

            foreach (Shopping shopping in shoppings)
            {
                ReduceShipment(shopping.Product, shopping.Quantity);
            }

            buyer.RemoveMoney(amountOfMoney);
            Money += amountOfMoney;
        }

        public void ReduceShipment(Product product, int quantity)
        {
            Shipment? shipmentInAssortment = assortment.Where(shipment => product.Id == shipment.Product.Id).Single();

            shipmentInAssortment.ReduceQuantity(quantity);
            if (shipmentInAssortment.Quantity == 0)
            {
                assortment.Remove(shipmentInAssortment);
            }
        }
    }
}
