using System;
using System.Collections.Generic;
using shopcart.Interfaces;

namespace shopcart
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly Dictionary<IProduct, int> products;
        private readonly double _baseTransportPrice;

        /// <summary>
        /// Crea una cistella de la compra a partir del preu base de transport
        /// </summary>
        /// <param name="transportPrice">Preu base del transport</param>
        public ShoppingCart(double transportPrice)
        {
            products = new Dictionary<IProduct, int>();
            _baseTransportPrice = transportPrice;
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

            var (pes, total) = CalculaPesIPreu();

            return total + CalculateTransport(pes, total);
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

        private (double , double) CalculaPesIPreu() {
            var pes = 0.0;
            var total = 0.0;
           foreach (var product in products)
           {
               var descompte = 0.0;
               var quantitat = product.Value;
               
               pes += quantitat * product.Key.Pes;
               if (quantitat >= 4) {
                   descompte = quantitat * (product.Key.Preu * 5 / 100);
               }
               total += quantitat * product.Key.Preu - descompte;
           }
           return (pes, Math.Round(total, 2));
        }

        private double CalculateTransport(double pes, double preu) 
        {
            if (preu >= 50) {
                return 0;
            }

            return pes > 5 ?  pes/5 + (_baseTransportPrice-1) : _baseTransportPrice;

        }

        public double TransportPrice
        {
            get
            {
                var (pes, preu) = CalculaPesIPreu();
                return CalculateTransport(pes, preu);
            }
        }

        public void Empty()
        {
            products.Clear();
        }

        public override string ToString() {
             var (pes, preu) = CalculaPesIPreu();
             return $"{string.Format("{0:0.##}",preu)} + {TransportPrice} euros, {pes} kg";
        }
    }

}