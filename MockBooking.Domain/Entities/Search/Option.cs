using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.Domain.Entities.Search
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        public string OptionCode { get; set; }
        public string HotelCode { get; set; }
        public string FlightCode { get; set; }
        public string ArrivalAirport { get; set; }
        public double Price { get; set; }
    }
}
