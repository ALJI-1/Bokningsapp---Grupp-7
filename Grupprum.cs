using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
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

        public static Grupprum SkapaNyttGrupprum() // Metod för att skapa ett nytt grupprum
        {
            int rumNr; // Variabel för att lagra rumnumret
            while (true)
            {
                Console.WriteLine("Ange nummer på grupprummet");
                string? inputRumNr = Console.ReadLine();
                if (!int.TryParse(inputRumNr, out rumNr)) // Kollar om inmatningen är korrekt.
                {
                    Console.WriteLine("Felaktig inmatning. Ange ett giltigt rumnummer");
                    inputRumNr = Console.ReadLine();
                }
                if (rumNr < 1 || rumNr > 100) // Kollar om rumsnumret är mellan 1 och 100
                {
                    Console.WriteLine("Rumnumret måste vara mellan 1 och 100");
                    inputRumNr = Console.ReadLine();
                }
                bool rumFinns = Program.lokaler.OfType<Grupprum>().Any(r => r.LokalNummer == rumNr); // Kollar om rumnumret redan finns
                if (rumFinns)
                {
                    Console.WriteLine("Rumnumret finns redan. Ange ett annat rumnummer");
                    inputRumNr = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            int rumKapacitet;
            while (true)
            {
                Console.WriteLine("Ange rummets kapacitet");
                string? inputRumKapacitet = Console.ReadLine(); // Variabel för att lagra kapaciteten
                if (!int.TryParse(inputRumKapacitet, out rumKapacitet)) // kollar om inmatningen är korrekt.
                {
                    Console.WriteLine("Felaktig inmatning.");
                    inputRumKapacitet = Console.ReadLine();
                }
                if (rumKapacitet < 2 || rumKapacitet > 20) // Bra gränser på kapaciteten?
                {
                    Console.WriteLine("Kapaciteten måste vara minst 2 och max 20");
                    inputRumKapacitet = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }

            bool harWhiteboard = BoolFråga("Har rummet whiteboard? (ja/nej)"); // Frågar om rummet har diverse med metoden BoolFråga
            bool harNödutgång = BoolFråga("Har rummet nödutgång? (ja/nej)");
            bool ärLjudisolerat = BoolFråga("Är rummet ljudisolerat? (ja/nej)");
            bool harTvSkärm = BoolFråga("Har rummet en TV-skärm? (ja/nej)");

            Grupprum nyttRum = new Grupprum(rumNr, rumKapacitet, harWhiteboard, harNödutgång, ärLjudisolerat, harTvSkärm); // Skapar en ny instans av Grupprum
            Program.lokaler.Add(nyttRum); // Lägger till det nya rummet i listan över lokaler
            Console.WriteLine("Ett nytt grupprum har skapats.");
            string sparadeLokaler = JsonSerializer.Serialize(Program.lokaler);
            File.WriteAllText("lokaler.json", sparadeLokaler);
            ClearConsole(); // Rensar konsollen
            return nyttRum; // Behövs denna returnering?
        }
    }
}
