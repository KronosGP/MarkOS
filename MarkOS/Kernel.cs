using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using MarkOS.Komendy;
using Cosmos.System.FileSystem;
using MarkOS.Hardware;
using MarkOS.Graphics;
using MarkOS.VM;


namespace MarkOS
{
    public class Kernel : Sys.Kernel
    {
        public static MenadzerKomend menadzer;
        private new_user user;
        private login login;
        private CosmosVFS FileSystem;
        public static string path = @"0:\";

        protected override void BeforeRun()
        {
            Inital();
            //Console.Clear();
            Console.WriteLine("This is MarkOS create by Adam Rychlik using Cosmos");
        }

        protected override void Run()
        {
            getInput();
        }
        public static void getInput()
        {
            Console.Write(path);
            var input = Console.ReadLine();
            var odpowiedz="";
            if (input == "clr") Console.Clear();
            else
            {
                odpowiedz = menadzer.Wejscie(input + " " + path);

            }
            if (input.Contains("dir")) zmianaFolderu(input, odpowiedz);
            if (!input.Contains("dir") || odpowiedz != "Czy napewno(Y/N)") Console.WriteLine(odpowiedz);

        }


        private void Inital()
        {
            Console.BackgroundColor = (ConsoleColor)Color.Green;
            Console.WriteLine("Uruchamianie systemu plikow");
            try
            {
                Delay(2000);
                this.FileSystem = new CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.FileSystem);
                Console.WriteLine("System plikow uruchominy");
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = (ConsoleColor)Color.Red;
                Console.WriteLine("Nie udalo sie uruchomic systemu plikow");
                Console.WriteLine("Reboot systemu za 10 sekund");
                Delay(10000);
                Sys.Power.Reboot();

            }

            Console.WriteLine("Uruchamianie komend");
            try
            {
                Delay(2000);
                menadzer = new MenadzerKomend();
                Console.WriteLine("Komendy uruchomione");
            }
            catch(Exception ex)
            {
                Console.BackgroundColor = (ConsoleColor)Color.Red;
                Console.WriteLine("Nie udalo sie uruchomic komend");
                Console.WriteLine("Reboot systemu za 10 sekund");
                Delay(10000);
                Sys.Power.Reboot();
            }
            
            Console.WriteLine("Uruchamianie debugera");
            try
            {
                Delay(2000);
                Runner.Initialize();
                Console.WriteLine("Debuger uruchomiony");
            }
            catch(Exception ex)
            {
                Console.BackgroundColor = (ConsoleColor)Color.Red;
                Console.WriteLine("Nie udalo sie uruchomic debugera");
                Console.WriteLine("Reboot systemu za 10 sekund");
                Delay(10000);
                Sys.Power.Reboot();
            }
            Console.BackgroundColor = (ConsoleColor)Color.Black;
            
            this.login = new login(menadzer, user);
            login.run();
        }

        private static void zmianaFolderu(string input, string odpowiedz)
        {
            if (input.Contains("dir cd..") == true)
                path = odpowiedz;
            else if (input.Contains("dir cd") == true && odpowiedz != "Folder nie istnieje" && odpowiedz != "")
                path += odpowiedz + @"\";
        }
        public static void Delay(int millis) { Cosmos.HAL.Global.PIT.Wait((uint)millis); }
    }
}

