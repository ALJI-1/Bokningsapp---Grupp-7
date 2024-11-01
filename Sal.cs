using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    // Subklass till Lokal
    public class Sal : Lokal
    {
        // Utöver de properties som finns i basklassen Lokal lägger vi till dessa properties
        public bool HarWebkamera { get; set; } 
        public bool HarBrandsläckare { get; set; }

        // Konstruktor
        public Sal(int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång, bool harWebkamera, bool harBrandsläckare) : base(LokalTyp.Sal, lokalNummer, kapacitet, harWhiteboard, harNödutgång)
        {
            HarWebkamera = harWebkamera;
            HarBrandsläckare = harBrandsläckare;
        }

    }
    
}
