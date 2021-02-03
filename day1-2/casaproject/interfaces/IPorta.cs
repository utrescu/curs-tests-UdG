namespace casaproject
{
    public interface IPorta
    {
        void Acciona();
        bool EsOberta();
        bool TeClau { get; set; }
        void GiraLaClau();
    }
}