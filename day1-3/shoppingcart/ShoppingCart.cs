using System;
using System.Collections.Generic;
using System.Linq;
using shopcart.Interfaces;

namespace shopcart
{
    public class ShoppingCart : IShoppingCart
    {
        private Dictionary<IProduct, int> products;
        private  readonly double _baseTransportPrice;

        private double pesBasePerDefecte = 5;

        private IUsuari _usuari;

        /// <summary>
        /// Crea una cistella de la compra a partir del preu base de transport
        /// </summary>
        /// <param name="transportPrice">Preu base del transport</param>
        /// <param name="usuari">usuari</param>
        public ShoppingCart(double transport, IUsuari usuari = null)
        {
            products = new Dictionary<IProduct, int>();
            _baseTransportPrice = transport;
            if (usuari != null) {
                _usuari = usuari;
            }
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

        public void RemoveProduct(int count, string product)
        {
            var key = products.Keys.FirstOrDefault(n => n.Nom == product);

            if (key == null)
            {
                throw new System.Exception("El producte no està a la cistella");
            }

            var quantity = products[key];
            if (quantity < count)
            {
                throw new System.Exception("Vols treure més productes dels que hi ha");
            }

            quantity -= count;
            if (quantity == 0)
            {
                products.Remove(key);
            }
            else
            {
                products[key] = quantity;
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
            if (preu >= 50
                || _usuari != null && _usuari.EsVIP()) {
                return 0;
            }

            if (pes >= pesBasePerDefecte ) {
                var afegir = Math.Round((pes + pesBasePerDefecte)/pesBasePerDefecte) - 1;
                return _baseTransportPrice + afegir;
            }

            return _baseTransportPrice;
        }

        public double TransportPrice
        {
            get
            {
                var (pes, preu) = CalculaPesIPreu();
                return CalculateTransport(pes, preu);
            }
        }

        public void Clear()
        {
            products.Clear();
        }

        public override string ToString() {
            var (pes, preu) = CalculaPesIPreu();
            return $"{string.Format("{0:0.##}",preu)} + {TransportPrice} euros, {pes} kg";
        }

        public string GetUsuari() => _usuari == null ? "anònim" : _usuari.Nom;

        public void AddUsuari(IUsuari usuari)
        {
            if (_usuari == null) {
                _usuari = usuari;
            } else {
                throw new Exception("Aquesta cistella ja pertany a un usuari");
            }
        }

        public List<IProduct> Items()
        {
            return new List<IProduct>(products.Keys);;
        }
    }

}