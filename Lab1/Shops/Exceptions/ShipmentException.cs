using System;

namespace Shops.Exceptions
{
    public class ShipmentException : Exception
    {
        private ShipmentException(string massage)
            : base(massage) { }
        public static ShipmentException InvalidCost(decimal cost)
        {
            return new ShipmentException($"Invalid cost: {cost}");
        }

        public static ShipmentException InvalidQuantity(int quantity)
        {
            return new ShipmentException($"Invalid quantityt: {quantity}");
        }

        public static ShipmentException NullArgument()
        {
            return new ShipmentException($"Argument == Null");
        }
    }
}
