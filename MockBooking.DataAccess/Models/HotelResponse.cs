using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.DataAccess.Models
{
    internal class HotelResponse
    {
        public int Id { get; set; }
        public int HotelCode { get; set; }
        public string HotelName { get; set; }
        public string DestinationCode { get; set; }
        public string City { get; set; }    
    }
}
