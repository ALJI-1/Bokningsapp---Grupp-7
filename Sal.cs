using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bokningsapp___Grupp_7
{
    // Subklass till Lokal
    public class Sal : Lokal
    {
        // Utöver de properties som finns i basklassen Lokal lägger vi till dessa properties
        public bool HarWebkamera { get; set; } 
        public bool HarBrandsläckare { get; set; }

        // Konstruktor som tar in alla properties, inklusive de från basklassen
        public Sal(int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång, bool harWebkamera, bool harBrandsläckare) 
            : base(LokalTyp.Sal, lokalNummer, kapacitet, harWhiteboard, harNödutgång)
        {
            HarWebkamera = harWebkamera;
            HarBrandsläckare = harBrandsläckare;
        }
        // Metod för att skapa en specifik lokal
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
                PrintInClolor("En ny sal har skapats.", ConsoleColor.Red);
                BokningsManager.SparaLokaler(); // Sparar lokaler till fil
                ClearConsole();
            }
        }

        // överskriden metod för att skriva ut text i färg
        protected override void PrintInClolor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }    
}
