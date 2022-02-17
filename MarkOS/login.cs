using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MarkOS.Komendy;

namespace MarkOS
{
    class login
    {
        String path = @"0:\sys\users.txt";
        List<String> loginy= new List<string>();

        public MenadzerKomend Menadzer;
        public new_user User;

        public login(MenadzerKomend menadzer, new_user user) 
        {
            Menadzer = menadzer;
            User = user;
            
        }
        internal void run()
        {
            Odczyt();
            bool zalogowany = false;
            do
            {
                string login, haslo;
                Console.Write("Login: ");
                login = Console.ReadLine();
                Console.Write("Haslo: ");
                haslo = Console.ReadLine();
                zalogowany = sprawdz(login, haslo);
            } while (zalogowany == false);
        }
        private bool sprawdz(string login, string haslo)
        {
            for(int i=0;i<loginy.Count;i+=2)
            {
                if (loginy[i] == login && loginy[i + 1] == haslo)
                    return true;
            }
            return false;
        }

        private void Odczyt()
        {
            if (Menadzer.Wejscie("plik ch "+path)=="nie")
                User.run("nowy");

            string temp = Menadzer.Wejscie("plik r "+path);
            string[] temp1 = temp.Split("\n");
            String[] split;
            for (int i = 0; i < temp1.Length; i++)
            {
                split = temp1[i].Split("#");
                loginy.Add(split[0]);
                loginy.Add(split[1]);
            }
            
            loginy.Add("admin");
            loginy.Add("admin");
        }
    }
}
