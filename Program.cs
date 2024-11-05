using System.Text.Json;

namespace Bokningsapp___Grupp_7
{
    public class Program
    {

        // Listor för att lagra bokningar och lokaler

        public static List<Lokal> bokningar = new List<Lokal>();

        public static List <Lokal> lokaler = new List<Lokal>();

        // Huvudmeyn för programmet. Lägg in metoder i switch-satsen för att anropa dem
        static void Main(string[] args)
        {
            string laddadeLokaler = File.ReadAllText("lokaler.json");
            lokaler = JsonSerializer.Deserialize<List<Lokal>>(laddadeLokaler);

            string sparadeBokningar = JsonSerializer.Serialize(bokningar);
            File.WriteAllText("bokningar.json", sparadeBokningar);

            string laddadeBokningar = File.ReadAllText("bokningar.json");
            bokningar = JsonSerializer.Deserialize<List<Lokal>>(laddadeBokningar);


            //Lokal sal1 = new Sal(1, 10, true, true, true, true);
            //Lokal grupprum1 = new Grupprum(2, 5, true, true, true, true);
            //lokaler.Add(sal1);
            //lokaler.Add(grupprum1);

            bool running = true;
            while (running)
            {
                Program program = new();
                program.PrintMenu(new string[] { "Boka lokal", "Visa bokningar", "Avboka", "Uppdatera bokning", "Visa lokaler", "Skapa ny lokal" });
                Console.Write("Välj ett alternativ: ");
                string? input = Console.ReadLine();
                Lokal lokal = new Lokal();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Boka lokal");
                        break;
                    case "2":
                        Console.WriteLine("Visa bokningar");
                        break;
                    case "3":
                        Console.WriteLine("Avboka");
                        break;
                    case "4":
                        Console.WriteLine("Uppdatera bokning");
                        break;
                    case "5":
                        lokal.VisaLokaler(lokaler);
                        break;
                    case "6":
                        lokal.SkapaNyLokal();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Felaktig inmatning");
                        break;
                }
            }
        }

        // Rör ej denna metod
        public void PrintMenu(String[] menuItems)
        {
            String topLeft = "╔";
            String topRight = "╗";
            String middleLeft = "╠";
            String middleRight = "╣";
            String bottomLeft = "╚";
            String bottomRight = "╝";
            String horizontal = "═";
            String vertical = "║";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(topLeft);
            for (int i = 0; i < 24; i++)
            {
                Console.Write(horizontal);
            }
            Console.WriteLine(topRight);
            Console.WriteLine(vertical + " Meny:".PadRight(24) + vertical);
            Console.Write(middleLeft);
            for (int i = 0; i < 24; i++)
            {
                Console.Write(horizontal);
            }
            Console.WriteLine(middleRight);


            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine(vertical + $"{i + 1} {menuItems[i]}".PadRight(24) + vertical);
            }
            Console.WriteLine(vertical + $"0 Avsluta".PadRight(24) + vertical);

            Console.Write(bottomLeft);
            for (int i = 0; i < 24; i++)
            {
                Console.Write(horizontal);
            }
            Console.WriteLine(bottomRight);


        }
    }


}



