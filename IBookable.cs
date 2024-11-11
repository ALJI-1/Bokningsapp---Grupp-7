using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    // Interface. Alla andra klasser som implementerar detta interface måste implementera metoderna nedan och ha dessa properties
    public interface IBookable 
    {
        public LokalTyp Typ { get; set; }
        public String? BokadAv { get; set; }
        public int LokalNummer { get; set; }
        public int Kapacitet { get; set; }


        // Deklarerar metoder som ska implementeras i klasser som implementerar interfacet. Ingen logik här
        public void SkapaBokning()
        {

        }
        public void AvbrytBokning()
        {


        }
        public void UppdateraBokning()
        {

        }
        public void VisaBokningar()
        {

        }
        public void SkapaNyLokal ()
        {

        }
    }
}
