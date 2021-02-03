using exemplePorta.Models;
using System;

namespace exemplePorta
{
    class Program
    {
        static void Main(string[] args)
        {
            // Porta sense clau
            var porta = new Porta();
            porta.Acciona();
            Console.WriteLine($"Porta oberta: {porta.EsOberta()}");

            // Porta amb clau
            var portaAmbClau = new Porta(true);
            porta.GiraLaClau();
            porta.Acciona();
            Console.WriteLine($"Porta oberta: {porta.EsOberta()}");
        }
    }
}
