using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Sys = Cosmos.System;
using MarkOS.Komendy;

namespace MarkOS
{
    class new_user
    {
        MenadzerKomend menadzer;

        String path = @"0:\sys\users.txt";
        public new_user(string nowy, MenadzerKomend menadzer)
        {
            this.menadzer = menadzer;
        }
        private void nowy(string nowy)
        {
            string login = "";
            string haslo = "";
            string typ = "";
            if (nowy == "nowy")
            {
                menadzer.Wejscie("plik mk  " + @"0:\sys\users.txt" + @"0:\sys\");
                Console.WriteLine("Nowy uzytkownik:");
                Console.Write("Podaj login ");
                login = Console.ReadLine();
                Console.Write("Podaj haslo ");
                haslo = Console.ReadLine();
                typ = "A";
            }
            else
            {
                Console.WriteLine("Nowy uzytkownik:");
                Console.Write("Podaj login ");
                login = Console.ReadLine();
                Console.Write("Podaj haslo ");
                haslo = Console.ReadLine();
                Console.Write("Podaj typ użytkownika \n'A'-administrtor\n'U'-uzytkownik\n'G'-gosc ");
                typ = Console.ReadLine();
            }
            Wpis(login, haslo, typ);
        }

        private void Wpis(string login, string haslo, string typ)
        {
            string text= login + "#" + haslo + "#" + typ + "\n";
            menadzer.Wejscie(path+" "+text);
            
        }

        internal void run(string v)
        {
            Console.WriteLine("v");
        }
    }
}
