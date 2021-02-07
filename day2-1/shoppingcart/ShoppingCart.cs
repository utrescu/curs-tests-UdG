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

        #region GestioCistella

        /// <summary>
        /// Determina si la cistella està buida
        /// </summary>
        /// <returns>True si la cistella és buida</returns>
        public bool IsEmpty() => GetItemsCount() == 0;

        /// <summary>
        /// Obtenir la llista d'articles de la cistella
        /// </summary>
        /// <returns></returns>
        public List<IProduct> Items()
        {
            return new List<IProduct>(products.Keys);
        }

        /// <summary>
        /// Obtenir el número de unitats de la cistella
        /// </summary>
        /// <returns>Quantitat d'items de la cistella</returns>
        public int GetItemsCount()
        {
            var totalItems = 0;
            foreach (var (_, count) in products)
            {
                totalItems += count;
            }
            return totalItems;
        }

        /// <summary>
        /// Buida la cistella de la compra
        /// </summary>
        public void EmptyShoppingCart()
        {
            products.Clear();
        }

        /// <summary>
        /// Afegeix un producte a la cistella
        /// </summary>
        /// <param name="count">quantitat</param>
        /// <param name="product">Producte que s'afegeix</param>
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
        
        /// <summary>
        /// Treure un article de la cistella
        /// </summary>
        /// <param name="count"></param>
        /// <param name="product"></param>
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

        #endregion

        #region Calcula

        /// <summary>
        /// Calcula el total a pagar segons les condicions de la cistella
        /// </summary>
        /// <returns>Quantitat a pagar</returns>
        public double GetTotal()
        {
            if (IsEmpty())
            {
                return 0;
            }

            var (pes, total) = CalculaPesIPreu();
            return total + CalculateTransport(pes, total);
        }


        /// <summary>
        /// Calcula el pes i el preu de venda dels productes de la cistella
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Calcula el transport dels productes de la cistella
        /// </summary>
        /// <param name="pes">Pes dels productes de la cistella</param>
        /// <param name="preu">Preu dels productes de la cistella</param>
        /// <returns></returns>
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

        /// <summary>
        /// Obtenir el preu del transport
        /// </summary>
        public double TransportPrice
        {
            get
            {
                var (pes, preu) = CalculaPesIPreu();
                return CalculateTransport(pes, preu);
            }
        }

        #endregion

        public override string ToString() {
            var (pes, preu) = CalculaPesIPreu();
            return $"{string.Format("{0:0.##}",preu)} + {TransportPrice} euros, {pes} kg";
        }

        #region usuari
        /// <summary>
        /// Obtenir l'usuari, en cas de que no n'hi hagi retorna anònim
        /// </summary>
        /// <returns></returns>
        public string GetUsuari() => _usuari == null ? "anònim" : _usuari.Nom;

        /// <summary>
        /// Afegir l'usuari a la cistella de la compra
        /// </summary>
        /// <param name="usuari"></param>
        public void AddUsuari(IUsuari usuari)
        {
            if (_usuari == null) {
                _usuari = usuari;
            } else {
                throw new Exception("Aquesta cistella ja pertany a un usuari");
            }
        }

        #endregion


    }

}