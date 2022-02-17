using System;
using System.Collections.Generic;
using System.Text;

namespace MarkOS.Komendy
{
    public class Komendy
    {
        public readonly String commend;

        public Komendy(String name) { this.commend = name; }

        public virtual String execute(String[] args)
        {

            return "";
        }
    }
}
