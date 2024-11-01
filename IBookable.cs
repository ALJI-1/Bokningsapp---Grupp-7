using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsapp___Grupp_7
{
    public interface IBookable
    {
        public int Nummer { get; set; }
        public int Kapacitet { get; set; }

        public void CancelBooking()
        {

        }
        public void CreateBooking()
        {

        }
        public void UpdateBooking()
        {

        }
        public void DisplayBookings()
        {

        }   


    }
    
}
