using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bokningsapp___Grupp_7
{
    // subklass till Lokal
    public class Grupprum : Lokal
    {
        // Utöver de properties som finns i basklassen Lokal lägger vi till dessa properties
        public bool ÄrLjudisolerat { get; set; }
        public bool HarTvSkärm { get; set; }

        // Konstruktor som tar in alla properties, inklusive de från basklassen
        public Grupprum(int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång, bool ärLjudisolerat, bool harTvSkärm) : base(LokalTyp.Grupprum, lokalNummer, kapacitet, harWhiteboard, harNödutgång)
        {
            ÄrLjudisolerat = ärLjudisolerat;
            HarTvSkärm = harTvSkärm;
        }

        // Metod för att skapa en specifik lokal
        protected override void SkapaSpecifikLokal(string typ)
        {
            if (typ == "grupprum")
            {
                // Frågar om rummet har diverse med metoden BoolFråga
                Console.Clear();
                bool harWhiteboard = BoolFråga("Har rummet whiteboard? (ja/nej)");
                bool harNödutgång = BoolFråga("Har rummet nödutgång? (ja/nej)");
                bool ärLjudisolerat = BoolFråga("Är rummet ljudisolerat? (ja/nej)");
                bool harTvSkärm = BoolFråga("Har rummet en TV-skärm? (ja/nej)");

                // Skapa en ny instans av Grupprum och spara den
                Grupprum nyttRum = new Grupprum(this.LokalNummer, this.Kapacitet, harWhiteboard, harNödutgång, ärLjudisolerat, harTvSkärm);

                BokningsManager.Lokaler.Add(nyttRum); // Lägger till det nya rummet i listan över lokaler
                Console.Clear();
                PrintInClolor("Ett nytt grupprum har skapats.", ConsoleColor.Yellow);
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
