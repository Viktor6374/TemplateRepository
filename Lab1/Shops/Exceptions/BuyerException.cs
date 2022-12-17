using System;

namespace Shops.Exceptions
{
    public class BuyerException : Exception
    {
        private BuyerException(string massage)
            : base(massage) { }
        public static BuyerException NegativeAmountOfMoney(decimal money)
        {
            return new BuyerException($"Negative amoune of money: {money}");
        }

        public static BuyerException NotEnoughMoney(decimal money, decimal moneyNeeded)
        {
            return new BuyerException($"Not Enough Money: money are there {money}, moneyNeeded {moneyNeeded}");
        }
    }
}
