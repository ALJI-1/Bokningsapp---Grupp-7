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
                if (!int.TryParse(Console.ReadLine(), out int lokalVal))
                {
                    Console.WriteLine("Felaktig inmatning. Försök igen.");
                    continue;
                }

                if (lokalVal == 0)
                {
                    Lokal.ClearConsole();
                    return;
                }

                if (lokalVal == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Lediga salar:");
                    foreach (var lokal in Lokaler.OfType<Sal>())
                    {
                        Console.WriteLine($"{lokal.Typ} {lokal.LokalNummer}");
                    }

                    Console.WriteLine("\nVilken sal vill du boka?");
                    if (!int.TryParse(Console.ReadLine(), out int salNr))
                    {
                        Console.WriteLine("Felaktig inmatning. Försök igen.");
                        continue;
                    }

                    var sal = Lokaler.OfType<Sal>().FirstOrDefault(s => s.LokalNummer == salNr);
                    if (sal != null)
                    {
                        Random randomBokNr = new Random();

                        Console.WriteLine("Ange ditt namn: ");
                        sal.BokadAv = Console.ReadLine();

                        Console.WriteLine("Ange startdatum (yyyy-MM-dd): ");
                        string? startDatum = Console.ReadLine();

                        Console.WriteLine("Ange starttid (HH:mm): ");
                        string? startKlocka = Console.ReadLine();

                        string? startTid = $"{startDatum} {startKlocka}";

                        int period = 0;
                        while (true)
                        {
                            Console.WriteLine("Hur många timmar (max 8) vill du boka: ");
                            int.TryParse(Console.ReadLine(), out period);
                            if (period > 8)
                            {
                                Console.WriteLine("Max 8 timmar kan bokas");
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }

                        try
                        {
                            sal.StartTid = DateTime.Parse(startTid);
                            sal.Period = TimeSpan.FromHours(period);
                            sal.SlutTid = sal.StartTid + sal.Period;

                            bool hasConflict = false;

                            foreach (var bokning in Bokningar)
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
                                } while (Bokningar.Any(b => b.BokningsNr == sal.BokningsNr));

                                Bokningar.Add(sal);
                                SparaBokningar();
                                Console.Clear();
                                Console.WriteLine($"Bokning skapad.\nDatum: {sal.StartTid:yyyy-MM-dd}");
                                Console.WriteLine($"Tid: {sal.StartTid:HH:mm} till {sal.SlutTid:HH:mm}");
                                Console.WriteLine($"Du har fått bokningsnummer: {sal.BokningsNr}");
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
                    foreach (var lokal in Lokaler.OfType<Grupprum>())
                    {
                        Console.WriteLine($"{lokal.Typ} {lokal.LokalNummer}");
                    }

                    Console.WriteLine("\nVilket grupprum vill du boka?");
                    if (!int.TryParse(Console.ReadLine(), out int gruppNr))
                    {
                        Console.WriteLine("Felaktig inmatning. Försök igen.");
                        continue;
                    }

                    var grupprum = Lokaler.OfType<Grupprum>().FirstOrDefault(s => s.LokalNummer == gruppNr);
                    if (grupprum != null)
                    {
                        Random randomBokNr = new Random();

                        Console.WriteLine("Ange ditt namn: ");
                        grupprum.BokadAv = Console.ReadLine();

                        Console.WriteLine("Ange startdatum (yyyy-MM-dd): ");
                        string? startDatum = Console.ReadLine();

                        Console.WriteLine("Ange starttid (HH:mm): ");
                        string? startKlocka = Console.ReadLine();

                        string? startTid = $"{startDatum} {startKlocka}";

                        Console.WriteLine("Hur många timmar (max 8) vill du boka: ");
                        int.TryParse(Console.ReadLine(), out int period);
                        if (period > 8)
                        {
                            Console.Clear();
                            Console.WriteLine("Max 8 timmar kan bokas");
                            Lokal.ClearConsole();
                            continue;
                        }

                        try
                        {
                            grupprum.StartTid = DateTime.Parse(startTid);
                            grupprum.Period = TimeSpan.FromHours(period);
                            grupprum.SlutTid = grupprum.StartTid + grupprum.Period;

                            bool hasConflict = false;

                            foreach (var bokning in Bokningar)
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
                                } while (Bokningar.Any(b => b.BokningsNr == grupprum.BokningsNr));

                                Bokningar.Add(grupprum);
                                SparaBokningar();
                                Console.Clear();
                                Console.WriteLine($"Bokning skapad.\nDatum: {grupprum.StartTid:yyyy-MM-dd}");
                                Console.WriteLine($"Tid: {grupprum.StartTid:HH:mm} till {grupprum.SlutTid:HH:mm}");
                                Console.WriteLine($"Du har fått bokningsnummer: {grupprum.BokningsNr}");
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
                        Console.WriteLine("Grupprum hittades inte");
                        Lokal.ClearConsole();
                    }
                }

            }
        }

        public static void VisaBokningar() // Metod för att visa de bokningar som finns och de bokningar som har varit // Rashid, Albin 
        {
            Console.Clear();
            // Kollar om det finns några bokningar
            if (Bokningar.Count == 0)
            {
                Console.WriteLine("Ingen bokningar finns för tillfället.");
                Lokal.ClearConsole();
                return;
            }
            Console.Clear();

            Console.WriteLine("--- Visa bokningar ---\n\nTryck 1 för att se alla bokningar.\n\nELLER\nSkriv in vilket år du vill se bokningar för.");
            String? input = Console.ReadLine();
            if (input == "1")
            {
                Console.Clear();
                Console.WriteLine("--- Kommande bokningar: ---\n");

                // Sorterar bokningarna i de som har varit och de som kommer. Jämför med dagens datum och tid
                var nuvarandeTid = DateTime.Now;
                var kommandeBokningar = Bokningar.Where(b => b.StartTid >= nuvarandeTid).OrderBy(b => b.StartTid).ToList();
                var förbiBokningar = Bokningar.Where(b => b.StartTid < nuvarandeTid).OrderBy(b => b.StartTid).ToList();

                // Loopar igenom varje kommande bokning i listan och skriver ut information
                foreach (var bokning in kommandeBokningar)
                {
                    Console.WriteLine($"Bokningsnummer: {bokning.BokningsNr}");
                    Console.WriteLine($"Bokad av: {bokning.BokadAv}");
                    Console.WriteLine($"Lokal: {bokning.Typ} {bokning.LokalNummer}");
                    Console.WriteLine($"Datum: {bokning.StartTid:yyyy-MM-dd}");
                    Console.WriteLine($"Tid: {bokning.StartTid:HH:mm} till {bokning.SlutTid:HH:mm}");
                    Console.WriteLine("------------------------"); // Avgränsare mellan bokningar
                }

                Console.WriteLine("\n--- Tidigare bokningar: ---\n");

                // Loopar igenom varje förbi bokning i listan och skriver ut information
                foreach (var bokning in förbiBokningar)
                {
                    Console.WriteLine($"Bokningsnummer: {bokning.BokningsNr}");
                    Console.WriteLine($"Bokad av: {bokning.BokadAv}");
                    Console.WriteLine($"Lokal: {bokning.Typ} {bokning.LokalNummer}");
                    Console.WriteLine($"Datum: {bokning.StartTid:yyyy-MM-dd}");
                    Console.WriteLine($"Tid: {bokning.StartTid:HH:mm} till {bokning.SlutTid:HH:mm}");
                    Console.WriteLine("------------------------");
                }

                Lokal.ClearConsole();
            }
            if (int.TryParse(input, out int year))
            {
                Console.Clear();
                var bookingYear = Bokningar.Where(b => b.StartTid?.Year == year).OrderBy(b => b.StartTid).ToList();
                if (bookingYear.Count > 0)
                {
                    foreach (var bokning in bookingYear)
                    {
                        Console.WriteLine($"Bokningsnummer: {bokning.BokningsNr}");
                        Console.WriteLine($"Bokad av: {bokning.BokadAv}");
                        Console.WriteLine($"Lokal: {bokning.Typ} {bokning.LokalNummer}");
                        Console.WriteLine($"Datum: {bokning.StartTid:yyyy-MM-dd}");
                        Console.WriteLine($"Tid: {bokning.StartTid:HH:mm} till {bokning.SlutTid:HH:mm}");
                        Console.WriteLine("------------------------");
                        
                    }
                    Console.ReadKey();
                }
            }
        }

        public void UppdateraBokning() // Metod för att uppdatera en bokning (exempelvis byta tid, byta lokal osv.) //RASHIID & CHRISTOFFER
        {
            Console.Clear();
            Console.WriteLine("Ange bokningsnummer: ");
            int.TryParse(Console.ReadLine(), out int bokNr);
            var bokning = Bokningar.Find(b => b.BokningsNr == bokNr);

            if (bokning != null)
            {
                Console.WriteLine("Bokning hittad");
                Console.WriteLine("Ange nytt startdatum (yyyy-MM-dd): ");
                string? nyttStartDatum = Console.ReadLine();

                Console.WriteLine("Ange ny starttid (HH-mm): ");
                string? nyStartKlocka = Console.ReadLine();

                string nyStartTid = $"{nyttStartDatum} {nyStartKlocka}";

                Console.WriteLine("Ange ny varaktighet (timmar): ");
                string? nyPeriod = Console.ReadLine();

                bokning.StartTid = DateTime.Parse(nyStartTid);
                bokning.Period = TimeSpan.FromHours(double.Parse(nyPeriod));
                bokning.SlutTid = bokning.StartTid + bokning.Period;

                Console.WriteLine("Bokning uppdaterad");
                SparaBokningar();
                Lokal.ClearConsole();
            }
            else
            {
                Console.WriteLine("Bokning hittades inte");
                Lokal.ClearConsole();
            }
        }
        public void AvbrytBokning() // Metod för att avboka en bokning som redan finns //RASHIID & CHRISTOFFER
        {
            Console.Clear();
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

                var bokning = Bokningar.Find(b => b.BokningsNr == bokNr);
                if (bokning != null)
                {
                    Bokningar.Remove(bokning);
                    Console.WriteLine("Bokning avbokad");
                    SparaBokningar();
                    Lokal.ClearConsole();
                    return;
                }
                else
                {
                    Console.WriteLine("Bokning hittades inte");
                    Lokal.ClearConsole();
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
                if (item != null)
                {
                    if (item["Typ"]?.ToString() == "1")
                        Lokaler.Add(item.Deserialize<Sal>());
                    else if (item["Typ"]?.ToString() == "0")
                        Lokaler.Add(item.Deserialize<Grupprum>());
                }
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
