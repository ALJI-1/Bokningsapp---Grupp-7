using System;
using System.Collections.Generic;
using System.Linq;
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

        }
        public void VisaBokningar() // Metod för att visa de bokningar som finns och de bokningar som har varit
        {

        }
        public void VisaLokaler() // Metod för att skriva ut information om alla lokaler
        {

        }
        public void SkapaNyLokal() // Metod för att skapa en ny lokal
        {

        }

        // Fler metoder kan läggas till här om det behövs

    }
}
