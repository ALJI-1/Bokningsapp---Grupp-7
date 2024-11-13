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

                if (lokalVal == 1) //kod som skannar all lediga sallar och visar
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

                    var sal = Lokaler.OfType<Sal>().FirstOrDefault(s => s.LokalNummer == salNr);   // lägger till valda salen på  listan med datumet/tiden som  bokad och namnet på användaren
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
                        // Försöker att skapa en bokning med de angivna värdena
                        try
                        {
                            sal.StartTid = DateTime.Parse(startTid);
                            sal.Period = TimeSpan.FromHours(period);
                            sal.SlutTid = sal.StartTid + sal.Period;

                            bool hasConflict = false;

                            foreach (var bokning in Bokningar)
                            {
                                // Kontrollerar om den nya bokningen krockar med någon annan bokning
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
                            // Om ingen dubbelbokning hittas, skapas bokningen
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
                        // Om felaktig inmatning, informera användaren och rensa konsolen
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
                if (lokalVal == 2) //kod som visar up lediga grupprum
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

                    var grupprum = Lokaler.OfType<Grupprum>().FirstOrDefault(s => s.LokalNummer == gruppNr);  // lägger till valda grupprum på  listan med datumet/tiden som  bokad och namnet på användaren
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
                        // Försöker att skapa en bokning med de angivna värdena
                        try
                        {
                            grupprum.StartTid = DateTime.Parse(startTid);
                            grupprum.Period = TimeSpan.FromHours(period);
                            grupprum.SlutTid = grupprum.StartTid + grupprum.Period;

                            bool hasConflict = false;
                            // Kontrollerar om den nya bokningen krockar med någon annan bokning
                            foreach (var bokning in Bokningar)
                            {
                                if (grupprum.StartTid < bokning.SlutTid &&
                                    grupprum.SlutTid > bokning.StartTid)
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
                            // Om ingen dubbelbokning hittas, skapas bokningen
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
        public void VisaBokningar() // Metod för att visa de bokningar som finns och de bokningar som har varit
        {
            Console.Clear();
            // Kollar om det överhuvudtaget finns några bokningar
            if (Bokningar.Count == 0)
            {
                Console.WriteLine("Ingen bokningar finns för tillfället.");
                Lokal.ClearConsole();
                return;
            }
            Console.Clear();

            // Användaren kan välja att se alla bokningar eller bokningar för ett specifikt år
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
            else if (int.TryParse(input, out int year))
            {
                Console.Clear();

                // Hämtar alla bokningar för det angivna året och sorterar dem efter starttid
                var bookingYear = Bokningar.Where(b => b.StartTid?.Year == year).OrderBy(b => b.StartTid).ToList();

                // Om det finns bokningar för det angivna året, skrivs de ut
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
                // Om inga bokningar hittas för det angivna året, skrivs det ut
                else
                {
                    Console.WriteLine("Inga bokningar hittades för det året.");
                    Lokal.ClearConsole();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Felaktig inmatning. Försök igen.");
                Lokal.ClearConsole();
            }
        }

        public void UppdateraBokning() // Metod för att uppdatera en bokning 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tryck 0 för att avbryta.\n");
                Console.WriteLine("Ange bokningsnummer: ");
                int.TryParse(Console.ReadLine(), out int bokNr);

                // Söker efter en bokning i listan 'Bokningar' baserat på det angivna bokningsnumret
                var nyBokning = Bokningar.Find(b => b.BokningsNr == bokNr);

                if (bokNr == 0)
                {
                    Lokal.ClearConsole();
                    return;
                }
                if (nyBokning != null)
                {
                    // Om bokningen hittas, skrivs informationen ut och användaren får möjlighet att uppdatera bokningen
                    Console.WriteLine("Bokning hittad");
                    Console.WriteLine("Ange nytt startdatum (yyyy-MM-dd): ");
                    string? nyttStartDatum = Console.ReadLine();
                    Console.WriteLine("Ange ny starttid (HH-mm): ");
                    string? nyStartKlocka = Console.ReadLine();
                    string nyStartTid = $"{nyttStartDatum} {nyStartKlocka}";

                    int nyPeriod = 0;
                    while (true)
                    {
                        Console.WriteLine("Hur många timmar (max 8) vill du boka: ");
                        int.TryParse(Console.ReadLine(), out nyPeriod);
                        if (nyPeriod > 8)
                        {
                            Console.WriteLine("Max 8 timmar kan bokas");
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    // Försöker att uppdatera bokningen med de nya värdena
                    try
                    {
                        nyBokning.StartTid = DateTime.Parse(nyStartTid);
                        nyBokning.Period = TimeSpan.FromHours(nyPeriod);
                        nyBokning.SlutTid = nyBokning.StartTid + nyBokning.Period;

                        bool hasConflict = false;

                        // Kontrollerar om den nya bokningen krockar med någon annan bokning
                        foreach (var bokningar in Bokningar)
                        {
                            if (bokningar.BokningsNr != nyBokning.BokningsNr && nyBokning.StartTid < bokningar.SlutTid && nyBokning.SlutTid > bokningar.StartTid)
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
                        // Om ingen dubbelbokning hittas, uppdateras bokningen
                        else
                        {
                            Bokningar.RemoveAll(b => b.BokningsNr == bokNr);
                            Bokningar.Add(nyBokning);
                            SparaBokningar();
                            Console.Clear();
                            Console.WriteLine($"Bokning uppdaterad.\nDatum: {nyBokning.StartTid:yyyy-MM-dd}");
                            Console.WriteLine($"Tid: {nyBokning.StartTid:HH:mm} till {nyBokning.SlutTid:HH:mm}");
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
                    Console.WriteLine("Bokning hittades inte");
                    Lokal.ClearConsole();
                }
            }
        }
        public void AvbrytBokning() // Metod för att avboka en bokning som redan finns 
        {
            Console.Clear();// Rensar konsolen för att ge en ren vy till användaren
            while (true)// En loop som fortsätter tills användaren avbryter eller en bokning hittas
            {
                Console.WriteLine("Tryck 0 för att avbryta.");// Informerar användaren om hur de kan avbryta processen
                Console.Write("Ange bokningsnummer: ");
                // Försöker läsa in och konvertera användarens inmatning till ett heltal (bokningsnummer)
                int.TryParse(Console.ReadLine(), out int bokNr);

                if (bokNr == 0) // Om användaren skriver 0, avbryts processen
                {
                    Lokal.ClearConsole();// Rensar konsolen via en metod i klassen Lokal
                    return;//Avsluta metod
                }
                // Söker efter en bokning i listan 'Bokningar' baserat på det angivna bokningsnumret
                var bokning = Bokningar.Find(b => b.BokningsNr == bokNr);
                if (bokning != null)// Kontroll om bokning hittas
                    
                {
                    Bokningar.Remove(bokning);// Ta bort bokning från listan bokningar
                 
                    Console.WriteLine("Bokning avbokad"); //Bekräftar att bokning avbokats

                    SparaBokningar();// Sparar änderningarna
                    Lokal.ClearConsole();// Rensar konsolen efter avbokning
                    return;//Avsluta metoden efter bokning har avbokats
                    
                }
                else
                {
                    // Om ingen bokning hittas med det angivna bokningsnumret, informeras användaren
                    Console.WriteLine("Bokning hittades inte");
                    Lokal.ClearConsole();//Rensar konsolen innan loopen start om
                }
            }

        }
        // Hämtar existerande lokaler från filen lokaler.json
        public static void LaddaLokaler()
        {
            if (!File.Exists("lokaler.json"))
            {
                File.WriteAllText("lokaler.json", "[]");
            }

            var jsonLokaler = File.ReadAllText("lokaler.json");
            var jsonList = JsonSerializer.Deserialize<List<JsonObject>>(jsonLokaler);

            Lokaler.Clear();
            // Loopar igenom varje JsonObject i listan och lägger till alla existerande lokaler med properies i listan Lokaler
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

        // Hämtar existerande bokningar från filen bokningar.json
        public static void LaddaBokningar()
        {
            
            if (!File.Exists("bokningar.json"))
            {
                File.WriteAllText("bokningar.json", "[]");
            }
            // Läser in json-filen och deserialiserar den till en lista av JsonObject
            var jsonBokningar = File.ReadAllText("bokningar.json");
            var jsonBokningarList = JsonSerializer.Deserialize<List<JsonObject>>(jsonBokningar);

            // Rensar listan Bokningar
            Bokningar.Clear();
            // Loopar igenom varje JsonObject i listan och lägger till alla existerande bokningar med properies i listan Bokningar
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

        // Sparar lokaler till filen lokaler.json
        public static void SparaLokaler()
        {
            string sparadeLokaler = JsonSerializer.Serialize(Lokaler);
            File.WriteAllText("lokaler.json", sparadeLokaler);
        }

        // Sparar bokningar till filen bokningar.json
        public static void SparaBokningar()
        {
            string sparadeBokningar = JsonSerializer.Serialize(Bokningar);
            File.WriteAllText("bokningar.json", sparadeBokningar);
        }
    }

}
