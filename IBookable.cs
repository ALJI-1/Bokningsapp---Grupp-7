using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    public interface IBookable
    {
        public LokalTyp Typ { get; set; }
        public int LokalNummer { get; set; }
        public int Kapacitet { get; set; }

        
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
        public void SkapaNyBokning ()
        {

        }
    }
}
