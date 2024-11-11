using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
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

        }
        public void AvbrytBokning() // Metod för att avboka en bokning som redan finns
        {

        }
        public void UppdateraBokning() // Metod för att uppdatera en bokning (exempelvis byta tid, byta lokal osv.)
        {

                            BokningsManager.Bokningar.Add(this);
                            BokningsManager.SparaBokningar();
                            Console.WriteLine($"Bokning skapad. Du har fått bokningsnummer: {BokningsNr}");
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

            {
                Console.WriteLine("Ange bokningsnummer: ");
                string? bokningsNummer = Console.ReadLine();
                var bokning = BokningsManager.Bokningar.Find(b => b.BokningsNr == BokningsNr);

                if (bokning != null)
                {
                    Console.WriteLine("Bokning hittad");
                }
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

            Console.WriteLine("Nuvarande bokningar:");

            // Loopar igenom varje bokning i listan och skriver ut information
            foreach (var bokning in BokningsManager.Bokningar)
            {
                Console.WriteLine($"Bokad av: {bokning.BokadAv}");

                // Om starttid och sluttid finns, skriv ut dem
                //int SlutTid = bokning.SlutTid;
                //if (bokning.StartTid && bokning.SlutTid)
                //{
                //    Console.WriteLine($"Starttid: {bokning.StartTid}");
                //    Console.WriteLine($"Sluttid: {bokning.SlutTid}");
                //}
                //else
                //{
                //    Console.WriteLine("Tid ej satt.");
                //}

                Console.WriteLine("------------"); // Avgränsare mellan bokningar
            }
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
        public void SkapaNyLokal() // Metod för att skapa en ny lokal
        {

        }

        // Fler metoder kan läggas till här om det behövs

    }
}
