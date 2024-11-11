using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
        public DateTime? StartTid { get; set; } // Bokningens starttid
        public DateTime? SlutTid { get; set; } // Bokningens sluttid
        public TimeSpan? Period { get; set; }  // Bokningens varaktighet
        public int BokningsNr { get; set; } // Bokningensnummer

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

        public void VisaLokaler(List<Lokal> lokaler) // Metod för att visa alla lokaler som finns // christoffer och albin
        {
            Console.Clear();

            if (lokaler.Count == 0) // Om det inte finns några lokaler
            {
                Console.WriteLine("Inga lokaler finns för tillfället.");
                ClearConsole();
                return;
            }

            // Sorterar lokaler efter lokaltyp förs, och sen efter lokalnummer
            lokaler = lokaler.OrderBy(l => l.Typ).ThenBy(l => l.LokalNummer).ToList();  

            foreach (var lokal in lokaler) // Loopar igenom alla lokaler och skriver ut information om dem (Grupprum kommer först, sen Salar - i nummerårdning)
            {
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
        // SkapaNyLokal - Christoffer och Albin
        public void SkapaNyLokal()
        {
            do
            {
                // Först får användaren välja vilken typ av lokal som ska skapas. Vi använder oss av en enum för att skilja på de olika typerna. Sätter värdet i propertyn Typ
                Console.Clear();
                Console.WriteLine("----- Skapa ny lokal ------\n\nAnge typ av lokal:\n\n1. för Sal\n2. för Grupprum\n\nValfri knapp för att gå tillbaka");
                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    break;
                }
                if (input == 1)
                {
                    Typ = LokalTyp.Sal;
                }
                else if (input == 2)
                {
                    Typ = LokalTyp.Grupprum;
                }
                else
                {
                    break;
                }

                // While-loop för att användaren ska kunna skapa en lokal tills den är nöjd med sitt val
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Ange lokalnummer:");
                    string number = Console.ReadLine();
                    int rumNr = int.Parse(number);

                    if (rumNr < 1 || rumNr > 100) // Kollar om rumsnumret är mellan 1 och 100
                    {
                        Console.WriteLine("Rumnumret måste vara mellan 1 och 100, försök igen.");

                        // Fördröjning för att användaren ska hinna läsa meddelandet
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        // Kollar om rumnumret redan finns på någon befintlig lokal
                        bool salFinns = BokningsManager.Lokaler.OfType<Sal>().Any(r => r.LokalNummer == rumNr); 
                        bool gruppRumFinns = BokningsManager.Lokaler.OfType<Grupprum>().Any(r => r.LokalNummer == rumNr); 

                        if (salFinns || gruppRumFinns)
                        {
                            Console.WriteLine("Rumnumret används redan. Ange ett annat rumnummer");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            // Sätter rumnumret i propertyn LokalNummer
                            LokalNummer = rumNr;

                            // While-loop för att användaren inte ska komma tillbaka till huvudmenyn om den skriver fel 
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine($"Skapar {Typ} {LokalNummer}\n\nKapasitet {Typ}: {(Typ == LokalTyp.Grupprum ? "2-20" : "10-200")}\n\nAnge sittplatser:");
                                string sittplatser = Console.ReadLine();
                                // Felhantering
                                if (!int.TryParse(sittplatser, out int capacity))
                                {
                                    Console.WriteLine("Ogiltigt antal sittplatser. Försök igen.");
                                    Thread.Sleep(1000);
                                    continue;
                                }

                                // Kollar att input matchar restiktionerna för antal sittplatser
                                if ((Typ == LokalTyp.Grupprum && (capacity < 2 || capacity > 20)) ||
                                        (Typ == LokalTyp.Sal && (capacity < 10 || capacity > 200)))
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Ogiltigt val. Antalet måste vara mellan intervallen: {(Typ == LokalTyp.Grupprum ? "2-20" : "10-200")}.");
                                    Console.ReadKey();                                 
                                }
                                else
                                { 
                                    // Sätter värdet i propertyn Kapacitet
                                    Kapacitet = capacity;

                                    // Skapa en instans av respektive subklass baserat på Typ och skicka med den input vi har fått från användaren
                                    if (Typ == LokalTyp.Sal)
                                    {
                                        var sal = new Sal(LokalNummer, Kapacitet, false, false, false, false);

                                        // Skickar med vår halvfärdiga lokal till metoden SkapaSpecifikLokal i subklassen Sal för att slutförade
                                        sal.SkapaSpecifikLokal("sal");
                                    }

                                    else if (Typ == LokalTyp.Grupprum)
                                    {
                                        var grupprum = new Grupprum(LokalNummer, Kapacitet, false, false, false, false);
                                        grupprum.SkapaSpecifikLokal("grupprum");
                                    }
                                    return; 
                                }                                
                            }                          
                        }
                    }
                }
            } while (true);
        }
        // Skapar en virtuell metod för att kunna skapa olika typer av lokaler.
        // Det enda den gör är att den skickar med användarens input och tillåter sig överskridas i subklasserna för att skapa lokaler med specifika egenskaper.
        protected virtual void SkapaSpecifikLokal(string typ) { }

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
