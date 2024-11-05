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

        // Konstruktor
        public Sal(int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång, bool harWebkamera, bool harBrandsläckare) : base(LokalTyp.Sal, lokalNummer, kapacitet, harWhiteboard, harNödutgång)
        {
            HarWebkamera = harWebkamera;
            HarBrandsläckare = harBrandsläckare;
        }

        public static Sal SkapaNySal() // Metod för att skapa en ny sal
        {
            int salNr; // Variabel för att lagra salnumret
            while (true)
            {
                Console.WriteLine("Ange salnummer");
                string? inputSalNr = Console.ReadLine();
                if (!int.TryParse(inputSalNr, out salNr)) // Kollar om inmatningen är korrekt.
                {
                    Console.WriteLine("Felaktig inmatning. Ange ett giltigt salnummer");
                    inputSalNr = Console.ReadLine();
                }
                if (salNr < 1 || salNr > 100) // Kollar om salnumret är mellan 1 och 100
                {
                    Console.WriteLine("Salnumret måste vara mellan 1 och 100");
                    inputSalNr = Console.ReadLine();
                }
                bool salFinns = Program.lokaler.OfType<Sal>().Any(s => s.LokalNummer == salNr); // Kollar om salnumret redan finns
                if (salFinns)
                {
                    Console.WriteLine("Salnumret finns redan. Ange ett annat salnummer");
                    inputSalNr = Console.ReadLine();
                }
                else
                {
                    
                    break;
                }
            }

            int salKapacitet;
            while (true)
            {
                Console.WriteLine("Ange salens kapacitet");
                string? inputSalKapacitet = Console.ReadLine(); // Variabel för att lagra kapaciteten
                if (!int.TryParse(inputSalKapacitet, out salKapacitet)) // kollar om inmatningen är korrekt.
                {
                    Console.WriteLine("Felaktig inmatning.");
                    inputSalKapacitet = Console.ReadLine();
                }
                if (salKapacitet < 10 || salKapacitet > 200) // Bra gränser på kapaciteten?
                {
                    Console.WriteLine("Kapaciteten måste vara minst 10 och max 200");
                    inputSalKapacitet = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }

            bool harWhiteboard = BoolFråga("Har salen en whiteboard? (ja/nej)"); // Frågar om salen har diverse med metoden BoolFråga
            bool harNödutgång = BoolFråga("Har salen en nödutgång? (ja/nej)");
            bool harWebkamera = BoolFråga("Har salen webkamera? (ja/nej)");
            bool harBrandsläckare = BoolFråga("Har salen brandsläckare? (ja/nej)");

            Sal nySal = new Sal(salNr, salKapacitet, harWhiteboard, harNödutgång, harWebkamera, harBrandsläckare); // Skapar en ny sal
            Program.lokaler.Add(nySal); // Lägger till salen i listan lokaler
            Console.WriteLine("En ny sal har skapats.");
            string sparadeLokaler = JsonSerializer.Serialize(Program.lokaler);
            File.WriteAllText("lokaler.json", sparadeLokaler);
            ClearConsole(); // Rensar konsollen
            return nySal; // Behövs denna?
        }

    }
    
}
