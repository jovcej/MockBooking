using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.DataAccess.Models
{
    public class FlightResponse
    {
        public int FlightCode { get; set; }
        public string PubliflightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
    }
}
