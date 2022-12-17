using Shops;
using Shops.Exceptions;
using Xunit;

namespace Shops.Test
{
    public class ShopServiceTest
    {
        [Fact]
        public void ShipmentProductToShop()
        {
            var service = new ShopService();
            Shop lenta = service.AddShop("Kronversky prospect 5", "Lenta");
            Product onion = service.CreateProduct("Onion");
            var delivery = new Delivery();
            service.AddToDelivery(onion, delivery, 20, 10);
            lenta.SetShipment(delivery);

            Assert.Equal(onion, service.Shops[0].Assortment[0].Product);
            Assert.Equal(service.Shops[0], lenta);
        }

        [Fact]
        public void SettingAndChangingPrices()
        {
            var service = new ShopService();
            Shop lenta = service.AddShop("Kronversky prospect 5", "Lenta");
            Product onion = service.CreateProduct("Onion");
            var delivery = new Delivery();
            service.AddToDelivery(onion, delivery, 20, 10);
            lenta.SetShipment(delivery);

            Assert.Equal(20, service.Shops[0].Assortment[0].Cost);

            lenta.ReduceShipment(onion, 10);
            var delivery2 = new Delivery();
            service.AddToDelivery(onion, delivery2, 25, 10);
            lenta.SetShipment(delivery2);

            Assert.Equal(25, service.Shops[0].Assortment[0].Cost);
        }

        [Fact]
        public void FindingLowPraces()
        {
            var service = new ShopService();
            Product onion = service.CreateProduct("Onion");

            Shop lenta = service.AddShop("Kronversky prospect 5", "Lenta");
            var delivery = new Delivery();
            service.AddToDelivery(onion, delivery, 20, 7);
            lenta.SetShipment(delivery);

            Shop okey = service.AddShop("Kronversky prospect 10", "Okey");
            var delivery2 = new Delivery();
            service.AddToDelivery(onion, delivery2, 25, 10);
            okey.SetShipment(delivery2);

            Assert.Equal(lenta, service.FindLowPrice(onion, 5));
            Assert.Equal(okey, service.FindLowPrice(onion, 9));
            Assert.Null(service.FindLowPrice(onion, 15));

            Product potato = service.CreateProduct("Potato");
            Assert.Null(service.FindLowPrice(potato, 5));
        }

        [Fact]
        public void PurchaseOfBatchOfGoods()
        {
            var service = new ShopService();
            Product onion = service.CreateProduct("Onion");
            Product potato = service.CreateProduct("Potato");
            Product carrot = service.CreateProduct("Carrot");

            Shop lenta = service.AddShop("Kronversky prospect 5", "Lenta");
            var delivery = new Delivery();
            service.AddToDelivery(onion, delivery, 20, 7);
            service.AddToDelivery(potato, delivery, 25, 10);
            service.AddToDelivery(carrot, delivery, 15, 15);
            lenta.SetShipment(delivery);

            var buyer = new Buyer(100);
            var purchase = new Purchase();
            service.AddToPurchase(purchase, carrot, 5);
            service.AddToPurchase(purchase, onion, 5);
            service.AddToPurchase(purchase, potato, 10);

            Assert.Throws<BuyerException>(() => service.Purchasing(buyer, purchase, lenta));

            buyer.AddMoney(900);

            service.Purchasing(buyer, purchase, lenta);
            decimal moneySpent = (5 * 15) + (5 * 20) + (10 * 25);
            Assert.Equal(1000 - moneySpent, buyer.Money);
            Assert.Equal(moneySpent, lenta.Money);
            int thereShouldBeOnion = 7 - 5;
            Assert.Equal(thereShouldBeOnion, lenta.Assortment[0].Quantity);

            Assert.Throws<ShoppingException>(() => service.Purchasing(buyer, purchase, lenta));
        }
    }
}
