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
            mesege((ConsoleColor)Color.Green, "Uruchamianie systemu plikow");
            try
            {
                Delay(2000);
                this.FileSystem = new CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.FileSystem);
                mesege((ConsoleColor)Color.Green, "System plikow uruchominy");
            }
            catch (Exception ex)
            {
                mesege((ConsoleColor)Color.Red, "Nie udalo sie uruchomic systemu plikow");
                Console.WriteLine("Reboot systemu za 10 sekund");
                Delay(10000);
                Sys.Power.Reboot();

            }

            mesege((ConsoleColor)Color.Green, "Uruchamianie komend");
            try
            {
                Delay(2000);
                menadzer = new MenadzerKomend();
                mesege((ConsoleColor)Color.Green, "Komendy uruchomione");
            }
            catch(Exception ex)
            {
                mesege((ConsoleColor)Color.Red, "Nie udalo sie uruchomic komend");
                Console.WriteLine("Reboot systemu za 10 sekund");
                Delay(10000);
                Sys.Power.Reboot();
            }
            
            mesege((ConsoleColor)Color.Green, "Uruchamianie debugera");
            try
            {
                Delay(2000);
                Runner.Initialize();
                mesege((ConsoleColor)Color.Green, "Debuger uruchomiony");
            }
            catch(Exception ex)
            {
                mesege((ConsoleColor)Color.Red, "Nie udalo sie uruchomic debugera");
                Console.WriteLine("Reboot systemu za 10 sekund");
                Delay(10000);
                Sys.Power.Reboot();
            }
            
            this.login = new login(menadzer, user);
            login.run();
        }

        private void mesege(ConsoleColor kolor, string v)
        {
            Console.Write("[");
            Console.BackgroundColor = kolor;
            Console.Write("    "+v+"    ");
            Console.BackgroundColor = (ConsoleColor)Color.Black;
            Console.WriteLine("]");
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

