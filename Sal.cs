using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    // Subklass till Lokal
    public class Sal : Lokal
    {
        // Utöver de properties som finns i basklassen Lokal lägger vi till dessa properties
        public bool HarWebkamera { get; set; } 
        public bool HarBrandsläckare { get; set; }

        // Konstruktorer
        public Sal(int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång, bool harWebkamera, bool harBrandsläckare) 
            : base(LokalTyp.Sal, lokalNummer, kapacitet, harWhiteboard, harNödutgång)
        {
            HarWebkamera = harWebkamera;
            HarBrandsläckare = harBrandsläckare;
        }
        public Sal()
        {

        }

        protected override void SkapaSpecifikLokal(string typ)
        {
            if (typ == "sal")
            {
                // Frågar om rummet har diverse med metoden BoolFråga
                Console.Clear();
                bool harWhiteboard = BoolFråga("Har rummet whiteboard? (ja/nej)"); 
                bool harNödutgång = BoolFråga("Har rummet nödutgång? (ja/nej)");
                bool harWebkamera = BoolFråga("Har salen webkamera? (ja/nej)"); ;
                bool harBrandsläckare = BoolFråga("Har salen brandsläckare? (ja/nej)");

                // Skapa en ny instans av Sal och spara den
                Sal nyttRum = new Sal(this.LokalNummer, this.Kapacitet, harWhiteboard, harNödutgång, harWebkamera, harBrandsläckare);

                BokningsManager.Lokaler.Add(nyttRum); // Lägger till det nya rummet i listan över lokaler
                Console.Clear();
                Console.WriteLine("En ny sal har skapats.");
                BokningsManager.SparaLokaler(); // Sparar lokaler till fil
                ClearConsole();
            }
        }
    }    
}
