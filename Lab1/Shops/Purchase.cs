using System.Collections.Generic;

namespace Shops
{
    public class Purchase
    {
        private List<Shopping> shoppingLists = new List<Shopping>();

        public IReadOnlyList<Shopping> ShoppingList => shoppingLists.AsReadOnly();
        public void AddShopping(Shopping shopping)
        {
            shoppingLists.Add(shopping);
        }

        public void RemoveShopping(Shopping shopping)
        {
            shoppingLists.Remove(shopping);
        }
    }
}
