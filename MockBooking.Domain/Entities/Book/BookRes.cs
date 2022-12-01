using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.Domain.Entities.Book
{
    public class BookRes
    {
        [Key]
        public int Id { get; set; }
        public string BookingCode { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
