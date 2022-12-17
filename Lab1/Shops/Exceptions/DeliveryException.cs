using System;

namespace Shops.Exceptions
{
    public class DeliveryException : Exception
    {
        private DeliveryException(string massage)
            : base(massage) { }
        public static DeliveryException EmptyDelivery()
        {
            return new DeliveryException($"Delivery Empty!");
        }

        public static DeliveryException NullArgument()
        {
            return new DeliveryException($"Argument == Null");
        }
    }
}
