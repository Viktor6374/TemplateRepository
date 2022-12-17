using System;
using Shops.Exceptions;

namespace Shops
{
    public class Product
    {
        public Product(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw ProductException.InvalidName(name);
            }

            Name = name;
        }

        public string Name { get; }
        public Guid Id { get; } = Guid.NewGuid();
    }
}
