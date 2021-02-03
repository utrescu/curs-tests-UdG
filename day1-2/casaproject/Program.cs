using System;
using System.Collections.Generic;

namespace casaproject
{
    class Program
    {
        static void Main(string[] args)
        {
            var persones = new List<IPersona> {
               new Persona {Nom="Pere", Sexe=Sexe.HOME},
               new Persona {Nom="Maria", Sexe=Sexe.HOME},
               new Persona {Nom="Frederic", Sexe=Sexe.HOME},
               new Persona {Nom="Filomena", Sexe=Sexe.DONA},
               new Persona {Nom="Mariano", Sexe=Sexe.HOME},
               new Persona {Nom="Marcela", Sexe=Sexe.DONA}
           };

            var porta = new Porta();
            var casa = new Casa(porta);

            casa.EntraPersona(persones[0]);
            casa.ObrePorta();
            casa.EntraPersona(persones[1]);
            casa.EntraPersona(persones[2]);
            casa.SurtPersona("Mariano");
            casa.SurtPersona("Maria");
            casa.TancaPorta();
            casa.EntraPersona(persones[3]);
            casa.SurtPersona("Frederic");

            System.Console.WriteLine($"hi ha: {casa.QuantesPersonesHiHa()} persones");
        }
    }
}
