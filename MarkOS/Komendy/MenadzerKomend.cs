using System;
using System.Collections.Generic;
using System.Text;

namespace MarkOS.Komendy
{
    
    public class MenadzerKomend
    {
        private List<Komendy> komendy;
        public MenadzerKomend()
        {
            this.komendy = new List<Komendy>(1);
            this.komendy.Add(new Pomoc("pomoc"));
            this.komendy.Add(new folder("folder"));
            this.komendy.Add(new dir("dir"));
            this.komendy.Add(new Wylacz("stop"));
            this.komendy.Add(new Reset("reboot"));
            this.komendy.Add(new plik("plik"));
            this.komendy.Add(new node("node"));
            this.komendy.Add(new debug("debug"));
            this.komendy.Add(new run("run"));
        }

        public String Wejscie (String input)
        {
            String[] split = input.Split(' ');
            String label = split[0];
            List<String> arg = new List<String>();
            int licz=0;
            foreach(String s in split)
            {
                if (licz != 0)
                    arg.Add(s);
                ++licz;
            }
            foreach(Komendy cmd in this.komendy)
            {
                if (cmd.commend == label)
                    return cmd.execute(arg.ToArray());
            }
            return "Komenda "+ label+" nie istnieje";
        }
    }
}
