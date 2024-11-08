using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    // Enum för att skilja på olika typer av lokaler när man skapar en ny lokal
    public enum LokalTyp
    {
        Grupprum,
        Sal
    }

    // Klassen Lokal (basklassen) som implementerar interfacet IBookable
    public class Lokal : IBookable
    {
        public String? BokadAv { get; set; } // Namnet på den som bokat lokalen
        public LokalTyp Typ { get; set; }   // Typen av lokal
        public int LokalNummer { get; set; } // Lokalens nummer som användaden skriver in
        public int Kapacitet { get; set; } // Lokalens kapacitet. Hur många sittplatser när man skapar ny lokal
        public bool HarWhiteboard { get; set; } // Om lokalen har whiteboard (bool)
        public bool HarNödutgång { get; set; } // Om lokalen har nödutgång (bool)
        public DateTime? StartTid { get; private set; } // Bokningens starttid
        public DateTime? SlutTid { get; private set; } // Bokningens sluttid
        public TimeSpan? Period { get; private set; }  // Bokningens varaktighet
        public int BokningsNr { get; set; }

        // ---------- Konstruktorer ----------
        public Lokal(LokalTyp typ, int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång) // Dessa parametrar måste sättas när en ny lokal skapas
        {
            Typ = typ;
            LokalNummer = lokalNummer;
            Kapacitet = kapacitet;
            HarWhiteboard = harWhiteboard;
            HarNödutgång = harNödutgång;
        }
        public Lokal() // Tom konstruktor om det behövs göras en instans av klassen utan att skicka in några värden
        {

        }

        // ---------- Metoder som ska implementeras från interfacet IBookable ----------

        public void SkapaBokning() // Metod för att skapa en bokning
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tryck 0 för att avbryta.\n");
                Console.WriteLine("Vilken typ av lokal vill du boka?\n1: Sal\n2: Grupprum");
                int.TryParse(Console.ReadLine(), out int lokalVal);

                if (lokalVal == 0)
                {
                    ClearConsole();
                    return;
                }

                if (lokalVal == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Lediga salar:");
                    foreach (var lokal in BokningsManager.Lokaler.OfType<Sal>())
                    {
                        Console.WriteLine($"{lokal.Typ} {lokal.LokalNummer}");
                    }

                    Console.WriteLine("\nVilken sal vill du boka?");
                    int.TryParse(Console.ReadLine(), out int salNr);

                    var sal = BokningsManager.Lokaler.OfType<Sal>().FirstOrDefault(s => s.LokalNummer == salNr);
                    if (sal != null)
                    {
                        Random randomBokNr = new Random();

                        Console.WriteLine("Ange ditt namn: ");
                        BokadAv = Console.ReadLine();

                        Console.WriteLine("Ange starttid (yyyy-MM-dd HH:mm): ");
                        string? startTid = Console.ReadLine();

                        Console.WriteLine("Hur många timmar vill du boka: ");
                        string? period = Console.ReadLine();
                        try
                        {
                            StartTid = DateTime.Parse(startTid);
                            Period = TimeSpan.FromHours(double.Parse(period));
                            SlutTid = StartTid + Period;
                            bool hasConflict = false;

                            foreach (var bokning in BokningsManager.Bokningar)
                            {
                                if (StartTid < bokning.SlutTid && SlutTid > bokning.StartTid)
                                {
                                    hasConflict = true;
                                    break;
                                }
                            }
                            if (hasConflict)
                            {
                                Console.Clear();
                                Console.WriteLine("Den nya bokningen krockar med befintliga bokningar. Försök igen");
                                ClearConsole();
                            }
                            else
                            {
                                do
                                {
                                    BokningsNr = randomBokNr.Next(100, 201);
                                } while (BokningsManager.Bokningar.Any(b => b.BokningsNr == BokningsNr));

                                BokningsManager.Bokningar.Add(this);
                                BokningsManager.SparaBokningar();
                                Console.Clear();
                                Console.WriteLine($"Bokning skapad. Tid: {StartTid} till {SlutTid}\nDu har fått bokningsnummer: {BokningsNr}");
                                ClearConsole();
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Felaktig inmatning. Försök igen.");
                            ClearConsole();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sal hittades inte");
                        ClearConsole();
                    }
                }
                if (lokalVal == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Lediga grupprum:");
                    foreach (var lokal in BokningsManager.Lokaler.OfType<Sal>())
                    {
                        Console.WriteLine($"{lokal.Typ} {lokal.LokalNummer}");
                    }

                    Console.WriteLine("\nVilket grupprum vill du boka?");
                    int.TryParse(Console.ReadLine(), out int salNr);

                    var sal = BokningsManager.Lokaler.OfType<Grupprum>().FirstOrDefault(s => s.LokalNummer == salNr);
                    if (sal != null)
                    {
                        Random randomBokNr = new Random();

                        Console.WriteLine("Ange ditt namn: ");
                        BokadAv = Console.ReadLine();

                        Console.WriteLine("Ange starttid (yyyy-MM-dd HH:mm): ");
                        string? startTid = Console.ReadLine();

                        Console.WriteLine("Hur många timmar vill du boka: ");
                        string? period = Console.ReadLine();

                        StartTid = DateTime.Parse(startTid);
                        Period = TimeSpan.FromHours(double.Parse(period));
                        SlutTid = StartTid + Period;
                        bool hasConflict = false;

                        foreach (var bokning in BokningsManager.Bokningar)
                        {
                            if (StartTid < bokning.SlutTid && SlutTid > bokning.StartTid)
                            {
                                hasConflict = true;
                                break;
                            }
                        }

                        if (hasConflict)
                        {
                            Console.Clear();
                            Console.WriteLine("Den nya bokningen krockar med befintliga bokningar. Försök igen");
                            ClearConsole();
                        }
                        else
                        {
                            do
                            {
                                BokningsNr = randomBokNr.Next(100, 201);
                            } while (BokningsManager.Bokningar.Any(b => b.BokningsNr == BokningsNr));

                            BokningsManager.Bokningar.Add(this);
                            BokningsManager.SparaBokningar();
                            Console.Clear();
                            Console.WriteLine($"Bokning skapad. Tid: {StartTid} till {SlutTid}\nDu har fått bokningsnummer: {BokningsNr}");
                            ClearConsole();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sal hittades inte");
                        ClearConsole();
                    }
                }
                
            }
        }
        public void AvbrytBokning() // Metod för att avboka en bokning som redan finns //RASHIID & CHRISTOFFER
        {
            while (true)
            {
                Console.WriteLine("Tryck 0 för att avbryta.");
                Console.Write("Ange bokningsnummer: ");
                int.TryParse(Console.ReadLine(), out int bokNr);

                if (bokNr == 0)
                { 
                    ClearConsole();
                    return;
                }

                var bokning = BokningsManager.Bokningar.Find(b => b.BokningsNr == bokNr);
                if (bokning != null)
                {
                    BokningsManager.Bokningar.Remove(bokning);
                    Console.WriteLine("Bokning avbokad");
                    BokningsManager.SparaBokningar();
                    ClearConsole();
                    return;
                }
                else
                {
                    Console.WriteLine("Bokning hittades inte");
                }
            }
            
        }
        public void UppdateraBokning() // Metod för att uppdatera en bokning (exempelvis byta tid, byta lokal osv.) //RASHIID & CHRISTOFFER
        {
            Console.WriteLine("Uppdatera bokning");
            Console.WriteLine("Ange namnet du bokade i: ");
            string? bokadAv = Console.ReadLine();
            var bokning = BokningsManager.Bokningar.Find(b => b.BokadAv == bokadAv);
            if (bokning != null)
            {
                Console.WriteLine("Bokning hittad");
                Console.WriteLine("Ange ny starttid (yyyy-MM-dd HH:mm): ");
                string? nyStartTid = Console.ReadLine();

                Console.WriteLine("Ange ny varaktighet (timmar): ");
                string? nyPeriod = Console.ReadLine();
                StartTid = DateTime.Parse(nyStartTid);
                Period = TimeSpan.FromHours(double.Parse(nyPeriod));

                Console.WriteLine("Bokning uppdaterad");
                BokningsManager.SparaBokningar();
            }
            else
            {
                Console.WriteLine("Bokning hittades inte");
            }
        }

        public void VisaBokningar() // Metod för att visa de bokningar som finns och de bokningar som har varit
        {

            // Kollar om det finns några bokningar
            if (BokningsManager.Bokningar.Count == 0)
            {
                Console.WriteLine("Ingen bokningar finns för tillfället.");
                return;
            }
            Console.Clear();
            Console.WriteLine("Nuvarande bokningar:\n");

            // Loopar igenom varje bokning i listan och skriver ut information
            foreach (var bokning in BokningsManager.Bokningar)
            {
                Console.WriteLine($"Bokad av: {bokning.BokadAv}");
                Console.WriteLine($"Starttid: {bokning.StartTid}");
                Console.WriteLine($"Sluttid: {bokning.SlutTid}");
                Console.WriteLine("------------"); // Avgränsare mellan bokningar
            }
            Console.ReadKey();
        }
        public void VisaLokaler(List<Lokal> lokaler) // Metod för att visa alla lokaler som finns // CHRISTOFFER
        {
            if (lokaler.Count == 0) // Om det inte finns några lokaler
            {
                Console.WriteLine("Inga lokaler finns för tillfället.");
                return;
            }

            foreach (var lokal in lokaler) // Loopar igenom alla lokaler och skriver ut information om dem
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Typ av lokal: {lokal.Typ}");
                Console.WriteLine($"Lokalnummer: {lokal.LokalNummer}");
                Console.WriteLine($"Kapacitet: {lokal.Kapacitet}");

                // Visa gemensamma egenskaper
                if (((Lokal)lokal).HarWhiteboard == true)
                {
                    Console.WriteLine("Har whiteboard: Ja");
                }
                else
                {
                    Console.WriteLine("Har whiteboard: Nej");
                }

                if (((Lokal)lokal).HarNödutgång == true)
                {
                    Console.WriteLine("Har nödutgång: Ja");
                }
                else
                {
                    Console.WriteLine("Har nödutgång: Nej");
                }

                // Skiljer mellan Sal och Grupprum för att visa specifika egenskaper
                if (lokal is Sal sal)
                {
                    if (((Sal)sal).HarWebkamera == true)
                    {
                        Console.WriteLine("Har webkamera: Ja");
                    }
                    else
                    {
                        Console.WriteLine("Har webkamera: Nej");
                    }

                    if (((Sal)sal).HarBrandsläckare == true)
                    {
                        Console.WriteLine("Har brandsläckare: Ja");
                    }
                    else
                    {
                        Console.WriteLine("Har brandsläckare: Nej");
                    }
                }
                else if (lokal is Grupprum grupprum)
                {
                    if (((Grupprum)grupprum).ÄrLjudisolerat == true)
                    {
                        Console.WriteLine("Är ljudisolerat: Ja");
                    }
                    else
                    {
                        Console.WriteLine("Är ljudisolerat: Nej");
                    }

                    if (((Grupprum)grupprum).HarTvSkärm == true)
                    {
                        Console.WriteLine("Har TV-skärm: Ja");
                    }
                    else
                    {
                        Console.WriteLine("Har TV-skärm: Nej");
                    }
                }

                Console.WriteLine("-------------------------------------------------");
              
            }
            ClearConsole();
        }

        public object SkapaNyLokal() // Metod för att skapa en ny lokal //CHRISTOFFER
        {
            Console.WriteLine("1: Skapa Sal\n2: Skapa Grupprum");
            string? input = Console.ReadLine();

            return input switch
            {
                "1" => Sal.SkapaNySal(), // Om användaren väljer 1 så anropas metoden SkapaNySal i klassen Sal
                "2" => Grupprum.SkapaNyttGrupprum(), // Om användaren väljer 2 så anropas metoden SkapaNyttGrupprum i klassen Grupprum
                _ => throw new ArgumentException("Felaktig inmatning. Ange 1 för Sal eller 2 för Grupprum")
            };

        }

        public static bool BoolFråga(string fråga)  //Hjälpmetod för att ställa en fråga som kräver ett ja/nej-svar //CHRISTOFFER
        {
            Console.Write(fråga + " ");
            string? svar = Console.ReadLine(); // Läser in svaret från användaren
            return svar?.ToLower() == "ja"; // Returnerar true om svaret är "ja" och false om svaret är "nej"
        }

        public static void ClearConsole() //Metod för att rensa konsolen. Metoden används på flera ställen i programmet för att förbättra användarvänligheten. //CHRISTOFFER
        {
            Console.WriteLine("\nTryck ENTER för att gå vidare.");
            Console.ReadLine();
            Console.Clear();
        }

        // Fler metoder kan läggas till här om det behövs
    }
}
