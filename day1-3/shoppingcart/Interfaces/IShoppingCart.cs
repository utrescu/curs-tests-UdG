namespace shopcart.Interfaces
{
    public interface IShoppingCart
    {
        void AddProduct(int count, IProduct product);
        void RemoveProduct(int count, IProduct product);
        double GetTotal();
        int GetItemsCount();
        bool IsEmpty();
        void Empty();
        double GetTransportPrice();
    }
}