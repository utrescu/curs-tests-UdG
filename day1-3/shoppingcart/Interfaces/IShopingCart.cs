namespace shoppingcart.Interfaces
{
    public interface IShopingCart
    {
        void AddProduct(int count, IProduct product);
        void RemoveProduct(int count, IProduct product);
        double GetTotal();
        int GetItemsCount();
        bool IsEmpty();
    }
}