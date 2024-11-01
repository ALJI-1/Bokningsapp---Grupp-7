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

        public Lokal(int nummer, int kapacitet, bool ärBokadNu, bool harWhiteboard, bool harNödutgång)
        {
            Nummer = nummer;
            Kapacitet = kapacitet;
            ÄrBokadNu = ärBokadNu;
            HarWhiteboard = harWhiteboard;
            HarNödutgång = harNödutgång;
        }
        public Lokal(int nummer, int kapacitet, bool harWhiteboard, bool harNödutgång)
        {
            Nummer = nummer;
            Kapacitet = kapacitet;
            HarWhiteboard = harWhiteboard;
            HarNödutgång = harNödutgång;
        }
        public Lokal()
        {

        }

    }
}
