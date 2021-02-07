namespace shopcart.Interfaces
{
    public interface IProduct
    {
        public string Nom { get; }
        public double Preu { get; set; }
        public double Pes { get; }
        public bool EsPesat();
    }
}