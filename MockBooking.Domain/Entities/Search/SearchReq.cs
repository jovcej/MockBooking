using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.Domain.Entities.Search
{
    public class SearchReq
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Destination { get; set; }
        public string DepartureAirport { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
    }
}
