using Shops.Exceptions;

namespace Shops
{
    public class Buyer
    {
        public Buyer(decimal money)
        {
            if (money < 0)
            {
                throw BuyerException.NegativeAmountOfMoney(money);
            }

            Money = money;
        }

        public decimal Money { get; private set; }

        public void AddMoney(decimal money)
        {
            if (money <= 0)
            {
                throw BuyerException.NegativeAmountOfMoney(money);
            }

            Money += money;
        }

        public void RemoveMoney(decimal money)
        {
            if (money <= 0)
            {
                throw BuyerException.NegativeAmountOfMoney(money);
            }

            if (Money < money)
            {
                throw BuyerException.NotEnoughMoney(Money, money);
            }

            Money -= money;
        }
    }
}
