using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;

namespace Bokningsapp___Grupp_7
{
    public class Program
    {
        // Huvudmeyn för programmet. Lägg in metoder i switch-satsen för att anropa dem
        static void Main(string[] args)
        {

            BokningsManager.LaddaLokaler();  // Laddar lokaler från fil
            BokningsManager.LaddaBokningar(); // Laddar bokningar från fil

            bool running = true;
            while (running)
            {
                Program program = new();
                program.PrintMenu(new string[] { "Boka lokal", "Visa bokningar", "Avboka", "Uppdatera bokning", "Visa lokaler", "Skapa lokal" });
                Console.Write("Välj ett alternativ: ");
                string? input = Console.ReadLine();
                Lokal lokal = new Lokal();
                BokningsManager bokningsManager = new BokningsManager();
                switch (input)
                {
                    case "1":
                        bokningsManager.SkapaBokning();
                        break;
                    case "2":
                        BokningsManager.VisaBokningar();
                        break;
                    case "3":
                        bokningsManager.AvbrytBokning();
                        break;
                    case "4":
                        bokningsManager.UppdateraBokning();
                        break;
                    case "5":
                        lokal.VisaLokaler(BokningsManager.Lokaler);
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

        // Metod för att skriva ut meny en snygg huvudmeny 
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