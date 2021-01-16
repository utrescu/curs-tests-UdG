using System.Collections.Generic;
using shopcart.Interfaces;

namespace shopcart
{
    public class ShoppingCart : IShoppingCart
    {
        private Dictionary<IProduct, int> products;
        private readonly double _transportPrice;

        public ShoppingCart(double transport)
        {
            products = new Dictionary<IProduct, int>();
            _transportPrice = transport;
        }

        public void AddProduct(int count, IProduct product)
        {
            if (products.ContainsKey(product))
            {
                var quantity = products[product] + count;
                products[product] = quantity;
            }
            else
            {
                products[product] = count;
            }
        }

        public int GetItemsCount()
        {
            var totalItems = 0;
            foreach (var (_, count) in products)
            {
                totalItems += count;
            }
            return totalItems;
        }

        public double GetTotal()
        {
            if (IsEmpty())
            {
                return 0;
            }

            var total = _transportPrice;
            foreach (var (producte, count) in products)
            {
                total = total + producte.Preu * count;
            }

            return total;
        }

        public bool IsEmpty() => GetItemsCount() == 0;

        public void RemoveProduct(int count, IProduct product)
        {
            if (!products.ContainsKey(product))
            {
                throw new System.Exception("El producte no està a la cistella");
            }

            var quantity = products[product];
            if (quantity < count)
            {
                throw new System.Exception("Vols treure més productes dels que hi ha");
            }

            quantity -= count;
            if (quantity == 0)
            {
                products.Remove(product);
            }
            else
            {
                products[product] = quantity;
            }
        }

        public double GetTransportPrice() => _transportPrice;

        public void Empty()
        {
            products.Clear();
        }
    }

}