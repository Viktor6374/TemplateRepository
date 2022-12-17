using System;

namespace Shops.Exceptions
{
    public class ShopException : Exception
    {
        private ShopException(string massage)
            : base(massage) { }

        public static ShopException NullArgument()
        {
            return new ShopException($"Argument == Null");
        }
    }
}
