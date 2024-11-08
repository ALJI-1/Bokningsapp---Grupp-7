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

                if (startTid.HasValue && slutTid.HasValue && period.HasValue && bokadAv != null && bokningsNr.HasValue)
                {
                    var lokal = new Lokal
                    {
                        StartTid = startTid.Value,
                        SlutTid = slutTid.Value,
                        Period = period.Value,
                        BokadAv = bokadAv,
                        BokningsNr = bokningsNr.Value
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
