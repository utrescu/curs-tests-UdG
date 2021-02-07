using System.Collections.Generic;

namespace shopcart.Interfaces
{
    public interface IShoppingCart
    {
        void AddProduct(int count, IProduct product);
        void RemoveProduct(int count, string product);
        int GetItemsCount();
        List<IProduct> Items();
        bool IsEmpty();
        void Clear();

        double GetTotal();
        double TransportPrice { get; }
        
        string GetUsuari();
        void AddUsuari(IUsuari usuari);
    }
}