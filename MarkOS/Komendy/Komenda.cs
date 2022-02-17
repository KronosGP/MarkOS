using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using MarkOS.Core;
using MarkOS.Hardware;
using MarkOS.Graphics;
using MarkOS.VM;

namespace MarkOS.Komendy
{
    
    public class dir : Komendy
    {
        public dir(String name) : base(name) { }
        public override string execute(string[] args)
        {
            string odpowiedz="";
            switch (args[0])
            {
                case "cd":
                    var path_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(args[2]);
                    foreach (var directoryEntry in path_list)
                    {
                        if (directoryEntry.mName.CompareTo(args[1]) == 0) { odpowiedz = args[1]; break; }
                        odpowiedz = "!";
                    }
                    if (odpowiedz == "!")
                        odpowiedz = "Folder nie istnieje";
                    

                    break;

                case "ch":
                    var path_list2 = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(args[1]);
                    foreach (var directoryEntry in path_list2)
                    {
                        odpowiedz += directoryEntry.mName +"\n";

                    }
                    break;
                case "cd..":
                    String[] split = args[1].Split(@"\");
                    if (split.Length > 1)
                    {
                        for (int i = 0; i < split.Length - 2; i++)
                        {
                            odpowiedz += split[i] + @"\";
                        }
                    }
                    else odpowiedz = @"0:\";
                    break;
                default: odpowiedz="nie oczekiwany argument " + args[0]; break;
            }
            return odpowiedz;
        }
    }


    public class plik : Komendy
    {
        public plik(String name) : base(name) { }
        public override string execute(string[] args)
        {
            String odpowiedz = "";
            String path;
            if (args[1].Contains(@"\") == false)
            {
                path = args[args.Length-1] + args[1];
            }
            else
                path = args[1];
            switch (args[0])
            {
                case "mk":
                    {
                        try
                        {
                            Sys.FileSystem.VFS.VFSManager.CreateFile(path);
                            odpowiedz = "Utworzyles plik " + args[1];
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }
                case "rm":
                    {
                        try
                        {
                            Sys.FileSystem.VFS.VFSManager.DeleteFile(path);
                            odpowiedz = "Usunoles plik " + args[1];
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }
                case "r":
                    {
                        try
                        {
                                odpowiedz = File.ReadAllText(path);
                            break;
                        }
                        catch(Exception ex)
                        {
                            odpowiedz = ex.ToString();
                            break;
                        }
                    }break;
                case "w":
                    {
                        try
                        {
                            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(path).GetFileStream();
                            if (fs.CanWrite)
                            {
                                Byte[] data = Encoding.ASCII.GetBytes(args[2]);
                                fs.Write(data, 0, data.Length);
                                fs.Close();
                            }
                            else
                            {
                                odpowiedz = "Nie udało sie wpisać tekstu. Nie jest otwarte dla zapisu";
                            }
                            
                            break;
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                            break;
                        }
                    }
                case "ch":
                    {
                        
                        if (Sys.FileSystem.VFS.VFSManager.FileExists(args[1]))
                            odpowiedz = "tak";
                        else
                            odpowiedz = "nie";
                        break;
                    }
                case "cp":
                    {
                        String path2;
                        if (args[2].Contains(@"\") == false)
                        {
                            path2 = args[args.Length - 1] + args[2];
                        }
                        else
                            path2 = args[2];
                        try
                        {
                            File.Copy(path, path2);
                        }
                        catch(Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }
                case "mv":
                    {
                        String path2;
                        if (args[2].Contains(@"\") == false)
                        {
                            path2 = args[args.Length - 1] + args[2];
                        }
                        else
                            path2 = args[2];
                        try
                        {
                            File.Copy(path, path2);
                            Sys.FileSystem.VFS.VFSManager.DeleteFile(path);
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }
                default:
                    { odpowiedz = "nie oczekiwany argument " + args[0]; break; }
            }
            return odpowiedz;
        }
    }

    public class folder : Komendy
    {
        public folder(String name) : base(name) { }
        public override string execute(string[] args)
        {
            String odpowiedz = "";
            String path;
            if (args[1].Contains(@"\") == false)
            {
                path = args[2] + args[1];
            }
            else
                path = args[1];
            switch (args[0])
            {
                case "mk":
                    {
                        try
                        {
                            Sys.FileSystem.VFS.VFSManager.CreateDirectory(path);
                            odpowiedz = "Utworzyles folder " + args[1];
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }
                case "rm":
                    {
                        try
                        {
                            Sys.FileSystem.VFS.VFSManager.DeleteDirectory(path, true);
                            odpowiedz = "Usunoles folder " + args[1];
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }
                case "cp":
                    {
                        String path2;
                        if (args[2].Contains(@"\") == false)
                        {
                            path2 = args[args.Length - 1] + args[2];
                        }
                        else
                            path2 = args[2];
                        try
                        {
                            File.Copy(path, path2);
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }
                case "mv":
                    {
                        String path2;
                        if (args[2].Contains(@"\") == false)
                        {
                            path2 = args[args.Length - 1] + args[2];
                        }
                        else
                            path2 = args[2];
                        try
                        {
                            File.Copy(path, path2);
                            Sys.FileSystem.VFS.VFSManager.DeleteFile(path);
                        }
                        catch (Exception ex)
                        {
                            odpowiedz = ex.ToString();
                        }
                        break;
                    }

                default:
                    { odpowiedz = "nie oczekiwany argument" + args[0]; break; }
            }
            return odpowiedz;
        }
    }

    public class node : Komendy
    {
        public node(String name) : base(name) { }

        public override string execute(string[] args)
        {
            if(args[0].Contains(@"\"))
                args[0] = args[args.Length - 1] + args[0];
            Looti.Run(args);
            return "";
        }
    }

    public class debug : Komendy
    {
        public debug(String name) : base(name) { }

        public override string execute(string[] args)
        {
            string odpowiedz="";
            bool success = false;
            string src = "", dest = "";
            if (args.Length == 3)
            {
                src = TryParseFile(args[0], true, args[args.Length-1]);
                dest = TryParseFile(args[1], false, args[args.Length - 1]);

                if (src != "*ERROR" && dest != "*ERROR") { success = true; }
                else { success = false; }
            }
            else { odpowiedz="Invalid argument! Path expected."; }

            if (success)
            {
                if (VM.Assembler.AssembleFile(src, dest))
                {
                    odpowiedz="Successfully assembled file";
                }
            }
            else
            {
                odpowiedz="Error assembling file";
            }
            return odpowiedz;
        }
        private static string TryParseFile(string file, bool exists,string path)
        {
            string realFile = file;
            if (file.Contains(@"\")) { realFile = file; }
            else realFile = path + file;
            if (exists)
            {
                if (Sys.FileSystem.VFS.VFSManager.FileExists(realFile)) { return realFile; }
                else { return "*ERROR"; }
            }
            else { return realFile; }
        }
    }

    public class run : Komendy
    {
        public run(String name) : base(name) { }

        public override string execute(string[] args)
        {
            string odpowiedz = "";
            string file = "", realFile = "*ERROR";
            if (args[0] == "d")
            {
                    CPU.DebugVisible = true;
                    realFile = TryParseFile(args[1], true, args[args.Length - 1]);
            }
            else
            {
                CPU.DebugVisible = false;
                realFile = TryParseFile(args[0], true,args[args.Length-1]);
            }

            if (realFile != "*ERROR")
            {
                if (realFile.ToUpper().EndsWith(".BIN") || realFile.ToUpper().EndsWith(".PRG"))
                {
                    try
                    {
                        byte[] data;
                        data = File.ReadAllBytes(realFile);
                        Runner.Reset(true);
                        Memory.WriteArray(0, data, data.Length);
                        Runner.Start();
                    }
                    catch (Exception ex)
                    {
                        CLI.WriteLine("Error occurred attempting to execute \"" + realFile + "\"", Color.Red);
                        CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message, Color.White);
                        Console.WriteLine("error");
                    }
                }
                else { CLI.WriteLine("File is not marked as executable", Color.Red); CLI.WriteLine("Expected file extension .BIN or .PRG", Color.White); }
            }
            else { CLI.WriteLine("Error occurred attempting to execute \"" + file + "\"", Color.Red); }
            return odpowiedz;
        }
        private static string TryParseFile(string file, bool exists, string path)
        {
            string realFile = file;
            if (file.Contains(@"\")) { realFile = file; }
            else realFile = path + file;
            if (exists)
            {
                if (Sys.FileSystem.VFS.VFSManager.FileExists(realFile)) { return realFile; }
                else { return "*ERROR"; }
            }
            else { return realFile; }
        }
    }


    public class Pomoc : Komendy
    {
        public Pomoc(String name) : base(name) { }

        public override string execute(string[] args)
        {
            return "System pomocy MarkOS wersja 1.0\n " +
                "<nazwa katalogu> h -pozwala sprawdzić informacje i instrukcje do podanego katalogu \n"+
                "dir -katalog zawierający instrukcje dotyczące ścieżki\n " +
                "folder -katalog zawierający instrukcje dotyczące folderów\n " +
                "plik -katalog zawierający instrukcje dotyczące plików\n ";
        }
    }

    public class Wylacz : Komendy
    {
        public Wylacz(String name) : base(name) { }

        public override string execute(string[] args)
        {
            Console.WriteLine("Czy napewno(Y/N)");
            String input = Console.ReadLine();
            if (input == "y" || input == "Y")
                Sys.Power.Shutdown();
            return "";
        }
    }

    public class Reset : Komendy
    {
        public Reset(String name) : base(name) { }

        public override string execute(string[] args)
        {
            Console.WriteLine("Czy napewno(Y/N)");
            String input = Console.ReadLine();
            if (input == "y" || input == "Y")
                Sys.Power.Reboot();
            return "";
        }
    }

}
