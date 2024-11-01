using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    // subklass till Lokal
    public class Grupprum : Lokal 
    {
        // Utöver de properties som finns i basklassen Lokal lägger vi till dessa properties
        public bool ÄrLjudisolerat { get; set; }
        public bool HarTvSkärm { get; set; }

        // Konstruktor
        public Grupprum(int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång, bool ärLjudisolerat, bool harTvSkärm) : base(LokalTyp.Grupprum, lokalNummer, kapacitet, harWhiteboard, harNödutgång)
        {
            ÄrLjudisolerat = ärLjudisolerat;
            HarTvSkärm = harTvSkärm;
        }
    }
}
