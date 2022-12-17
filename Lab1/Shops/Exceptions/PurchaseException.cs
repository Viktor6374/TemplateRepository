using System;

namespace Shops
{
    public class PurchaseException : Exception
    {
        private PurchaseException(string massage)
            : base(massage) { }

        public static PurchaseException NullArgument()
        {
            return new PurchaseException($"Argument == Null");
        }
    }
}
