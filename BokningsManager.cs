using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    public class BokningsManager  // Klass för att hantera bokningar och lokaler
    {
        public static List<Lokal> Lokaler { get; private set; } = new List<Lokal>(); // Lista för att lagra lokaler
        public static List<Lokal> Bokningar { get; private set; } = new List<Lokal>(); // Lista för att lagra bokningar

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
                    Lokal.ClearConsole();
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
                        sal.BokadAv = Console.ReadLine();

                        Console.WriteLine("Ange starttid (yyyy-MM-dd HH:mm): ");
                        string? startTid = Console.ReadLine();

                        Console.WriteLine("Hur många timmar vill du boka: ");
                        string? period = Console.ReadLine();
                        try
                        {
                            sal.StartTid = DateTime.Parse(startTid);
                            sal.Period = TimeSpan.FromHours(double.Parse(period));
                            sal.SlutTid = sal.StartTid + sal.Period;
                            bool hasConflict = false;

                            foreach (var bokning in BokningsManager.Bokningar)
                            {
                                if (sal.StartTid < bokning.SlutTid && sal.SlutTid > bokning.StartTid)
                                {
                                    hasConflict = true;
                                    break;
                                }
                            }
                            if (hasConflict)
                            {
                                Console.Clear();
                                Console.WriteLine("Den nya bokningen krockar med befintliga bokningar. Försök igen");
                                Lokal.ClearConsole();
                            }
                            else
                            {
                                do
                                {
                                    sal.BokningsNr = randomBokNr.Next(100, 201);
                                } while (BokningsManager.Bokningar.Any(b => b.BokningsNr == sal.BokningsNr));

                                BokningsManager.Bokningar.Add(sal);
                                BokningsManager.SparaBokningar();
                                Console.Clear();
                                Console.WriteLine($"Bokning skapad. Tid: {sal.StartTid} till {sal.SlutTid}\nDu har fått bokningsnummer: {sal.BokningsNr}");
                                Lokal.ClearConsole();
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Felaktig inmatning. Försök igen.");
                            Lokal.ClearConsole();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sal hittades inte");
                        Lokal.ClearConsole();
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

                    var grupprum = BokningsManager.Lokaler.OfType<Grupprum>().FirstOrDefault(s => s.LokalNummer == salNr);
                    if (grupprum != null)
                    {
                        Random randomBokNr = new Random();

                        Console.WriteLine("Ange ditt namn: ");
                        grupprum.BokadAv = Console.ReadLine();

                        Console.WriteLine("Ange starttid (yyyy-MM-dd HH:mm): ");
                        string? startTid = Console.ReadLine();

                        Console.WriteLine("Hur många timmar vill du boka: ");
                        string? period = Console.ReadLine();

                        grupprum.StartTid = DateTime.Parse(startTid);
                        grupprum.Period = TimeSpan.FromHours(double.Parse(period));
                        grupprum.SlutTid = grupprum.StartTid + grupprum.Period;
                        bool hasConflict = false;

                        foreach (var bokning in BokningsManager.Bokningar)
                        {
                            if (grupprum.StartTid < bokning.SlutTid && grupprum.SlutTid > bokning.StartTid)
                            {
                                hasConflict = true;
                                break;
                            }
                        }

                        if (hasConflict)
                        {
                            Console.Clear();
                            Console.WriteLine("Den nya bokningen krockar med befintliga bokningar. Försök igen");
                            Lokal.ClearConsole();
                        }
                        else
                        {
                            do
                            {
                                grupprum.BokningsNr = randomBokNr.Next(100, 201);
                            } while (BokningsManager.Bokningar.Any(b => b.BokningsNr == grupprum.BokningsNr));

                            BokningsManager.Bokningar.Add(grupprum);
                            BokningsManager.SparaBokningar();
                            Console.Clear();
                            Console.WriteLine($"Bokning skapad. Tid: {grupprum.StartTid} till {grupprum.SlutTid}\nDu har fått bokningsnummer: {grupprum.BokningsNr}");
                            Lokal.ClearConsole();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sal hittades inte");
                        Lokal.ClearConsole();
                    }
                }

            }
        }

        public void VisaBokningar() // Metod för att visa de bokningar som finns och de bokningar som har varit // Rashid, Albin 
        {
            // Kollar om det finns några bokningar
            if (BokningsManager.Bokningar.Count == 0)
            {
                Console.WriteLine("Ingen bokningar finns för tillfället.");
                Lokal.ClearConsole();
                return;
            }
            Console.Clear();

            Console.WriteLine("--- Visa bokningar ---\n\n Tryck 1 för att se alla bokningar eller skriv in vilket år du vill se bokningar för.");
            String? input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("--- Kommande bokningar: ---\n");

                // Sorterar bokningarna i de som har varit och de som kommer. Jämför med dagens datum och tid
                var nuvarandeTid = DateTime.Now;
                var kommandeBokningar = BokningsManager.Bokningar.Where(b => b.StartTid >= nuvarandeTid).OrderBy(b => b.StartTid).ToList();
                var förbiBokningar = BokningsManager.Bokningar.Where(b => b.StartTid < nuvarandeTid).OrderBy(b => b.StartTid).ToList();

                // Loopar igenom varje kommande bokning i listan och skriver ut information
                foreach (var bokning in kommandeBokningar)
                {
                    Console.WriteLine($"Bokningsnummer: {bokning.BokningsNr}");
                    Console.WriteLine($"Bokad av: {bokning.BokadAv}");
                    Console.WriteLine($"Starttid: {bokning.StartTid}");
                    Console.WriteLine($"Sluttid: {bokning.SlutTid}");
                    Console.WriteLine("------------------------"); // Avgränsare mellan bokningar
                }

                Console.WriteLine("\n--- Tidigare bokningar: ---\n");

                // Loopar igenom varje förbi bokning i listan och skriver ut information
                foreach (var bokning in förbiBokningar)
                {
                    Console.WriteLine($"Bokningsnummer: {bokning.BokningsNr}");
                    Console.WriteLine($"Bokad av: {bokning.BokadAv}");
                    Console.WriteLine($"Starttid: {bokning.StartTid}");
                    Console.WriteLine($"Sluttid: {bokning.SlutTid}");
                    Console.WriteLine("------------------------");
                }

                Lokal.ClearConsole();
            }
            else if (int.TryParse(input, out int year))
            {
                var bookingYear = BokningsManager.Bokningar.Where(b => b.StartTid?.Year == year).OrderBy(b => b.StartTid).ToList();
                if (bookingYear.Count > 0)
                {
                    foreach (var bokning in bookingYear)
                    {
                        Console.WriteLine($"Bokningsnummer: {bokning.BokningsNr}");
                        Console.WriteLine($"Bokad av: {bokning.BokadAv}");
                        Console.WriteLine($"Starttid: {bokning.StartTid}");
                        Console.WriteLine($"Sluttid: {bokning.SlutTid}");
                        Console.WriteLine("------------------------");

                    }
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
                bokning.StartTid = DateTime.Parse(nyStartTid);
                bokning.Period = TimeSpan.FromHours(double.Parse(nyPeriod));
                bokning.SlutTid = bokning.StartTid + bokning.Period;

                Console.WriteLine("Bokning uppdaterad");
                BokningsManager.SparaBokningar();
            }
            else
            {
                Console.WriteLine("Bokning hittades inte");
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
                    Lokal.ClearConsole();
                    return;
                }

                var bokning = BokningsManager.Bokningar.Find(b => b.BokningsNr == bokNr);
                if (bokning != null)
                {
                    BokningsManager.Bokningar.Remove(bokning);
                    Console.WriteLine("Bokning avbokad");
                    BokningsManager.SparaBokningar();
                    Lokal.ClearConsole();
                    return;
                }
                else
                {
                    Console.WriteLine("Bokning hittades inte");
                }
            }

        }
        public static void LaddaLokaler()
        {
            var jsonLokaler = File.ReadAllText("lokaler.json");
            var jsonList = JsonSerializer.Deserialize<List<JsonObject>>(jsonLokaler);

            Lokaler.Clear();
            foreach (var item in jsonList)
            {
                if (item["Typ"]?.ToString() == "1")
                    Lokaler.Add(item.Deserialize<Sal>());
                else if (item["Typ"]?.ToString() == "0")
                    Lokaler.Add(item.Deserialize<Grupprum>());
            }
        }

        public static void LaddaBokningar()
        {
            var jsonBokningar = File.ReadAllText("bokningar.json");
            var jsonBokningarList = JsonSerializer.Deserialize<List<JsonObject>>(jsonBokningar);

            Bokningar.Clear();
            foreach (var item in jsonBokningarList)
            {
                var startTid = item["StartTid"]?.GetValue<DateTime>();
                var slutTid = item["SlutTid"]?.GetValue<DateTime>();
                TimeSpan? period = null;
                if (item["Period"] != null)
                {
                    period = TimeSpan.Parse(item["Period"].ToString());
                }

                var bokadAv = item["BokadAv"]?.ToString();
                var bokningsNr = item["BokningsNr"]?.GetValue<int>();
                var lokalTyp = (LokalTyp)Enum.Parse(typeof(LokalTyp), item["Typ"]?.ToString());
                var lokalNummer = item["LokalNummer"]?.GetValue<int>();

                if (startTid.HasValue && slutTid.HasValue && period.HasValue && bokadAv != null && bokningsNr.HasValue && lokalNummer.HasValue)
                {
                    var lokal = new Lokal
                    {
                        StartTid = startTid.Value,
                        SlutTid = slutTid.Value,
                        Period = period.Value,
                        BokadAv = bokadAv,
                        BokningsNr = bokningsNr.Value,
                        Typ = lokalTyp,
                        LokalNummer = lokalNummer.Value
                    };
                    Bokningar.Add(lokal);
                }
            }
        }


        public static void SparaLokaler()
        {
            string sparadeLokaler = JsonSerializer.Serialize(Lokaler);
            File.WriteAllText("lokaler.json", sparadeLokaler);
        }

        public static void SparaBokningar()
        {
            string sparadeBokningar = JsonSerializer.Serialize(Bokningar);
            File.WriteAllText("bokningar.json", sparadeBokningar);
        }
    }

}
