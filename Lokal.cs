using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    public class Lokal : IBookable
    {
        public int Nummer { get; set; }
        public int Kapacitet { get; set; }
        public bool ÄrBokadNu { get; set; }
        public bool HarWhiteboard { get; set; }
        public bool HarNödutgång { get; set; }


    }
}
