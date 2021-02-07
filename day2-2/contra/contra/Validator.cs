using System;

namespace contra
{
    public class Validator
    {
        private const string numeros = "0123456789";
        private const string minuscules = "abcçdefghijklmnopqrstuvwxyz";
        private const string majuscules = "ABCDEFGHIJKLMNOPQRSTUVWXYZÇ";

        public Validator(string username)
        {
            Username = username;
        }

        public string Username { get; }

        public bool Valida(string contrasenya)
        {
            if (contrasenya.ToLower() == Username.ToLower() 
                || contrasenya.ToLower() == reverse(Username))
            {
                return false;
            }
            
            if (contrasenya.Length < 8 
                 || ComptaNumeros(contrasenya) < 2
                 || !ConteMinuscules(contrasenya)
                 || !ConteMajuscules(contrasenya)
                ) 
            {
                return false;
            }

            return true;
        }

        private string reverse(string username)
        {
            var resultat = "";
            foreach(var c in username.ToLower()) {
                resultat = c + resultat;
            }
            return resultat;
        }

        private bool  ConteMinuscules(string contrasenya)
        {
            foreach (var caracter in contrasenya)
            {
                if (minuscules.Contains(caracter))
                {
                   return true;
                }
            }

            return false;
        }

        private bool  ConteMajuscules(string contrasenya)
        {
            foreach (var caracter in contrasenya)
            {
                if (majuscules.Contains(caracter))
                {
                   return true;
                }
            }

            return false;
        }
        private static int ComptaNumeros(string contrasenya)
        {
            var nums = 0;
            var usats = "";
            foreach (var caracter in contrasenya)
            {
                if (numeros.Contains(caracter))
                {
                    if (!usats.Contains(caracter)) {
                        usats += caracter;
                        nums++;
                    }
                }
            }

            return nums;
        }


    }
}
