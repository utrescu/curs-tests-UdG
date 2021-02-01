namespace shopcart.Interfaces
{
    public interface IShoppingCart
    {
        void AddProduct(int count, IProduct product);
        void RemoveProduct(int count, IProduct product);
        double GetTotal();
        int GetItemsCount();
        bool IsEmpty();
        void Clear();
        double TransportPrice { get; }
        string GetUsuari();
        void AddUsuari(IUsuari usuari);
    }
}