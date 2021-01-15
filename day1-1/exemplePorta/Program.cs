using exemplePorta.Models;
using System;

namespace exemplePorta
{
    class Program
    {
        static void Main(string[] args)
        {
            var porta = new Porta();
            porta.Acciona();
            Console.WriteLine($"Porta oberta: {porta.EsOberta()}");
        }
    }
}
