using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    public enum LokalTyp
    {
        Grupprum,
        Sal
    }
    public class Lokal : IBookable
    {
        public LokalTyp Typ { get; set; }
        public int LokalNummer { get; set; }
        public int Kapacitet { get; set; }
        public bool ÄrBokadNu { get; set; }
        public bool HarWhiteboard { get; set; }
        public bool HarNödutgång { get; set; }

        public Lokal(LokalTyp typ, int lokalNummer, int kapacitet, bool ärBokadNu, bool harWhiteboard, bool harNödutgång)
        {
            Typ = typ;
            LokalNummer = lokalNummer;
            Kapacitet = kapacitet;
            ÄrBokadNu = ärBokadNu;
            HarWhiteboard = harWhiteboard;
            HarNödutgång = harNödutgång;
        }
        public Lokal(LokalTyp typ, int lokalNummer, int kapacitet, bool harWhiteboard, bool harNödutgång)
        {
            Typ = typ;
            LokalNummer = LokalNummer;
            Kapacitet = kapacitet;
            HarWhiteboard = harWhiteboard;
            HarNödutgång = harNödutgång;
        }
        public Lokal()
        {

        }

        public void SkapaBokning()
        {

        }
        public void AvbrytBokning()
        {

        }
        public void UppdateraBokning()
        {

        }
        public void VisaBookningar()
        {

        }
        public void SkapaNyLokal()
        {

        }


    }
}
