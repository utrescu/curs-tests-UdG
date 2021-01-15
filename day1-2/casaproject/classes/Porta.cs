namespace casaproject
{
    public class Porta
    {
        public bool oberta { get; set; }

        public void Obre()
        {
            if (!EstaOberta())
            {
                oberta = true;
            }
        }

        public void Tanca()
        {
            oberta = false;
        }

        public bool EstaOberta()
        {
            return oberta;
        }
    }
}