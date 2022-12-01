using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.Domain.Entities.Book
{
    public class BookedReservation 
    {
        public int Id { get; set; }
        public BookReq Request { get; set; }
        public BookRes Response { get; set; }
        public int SleepTime { get; set; }
    }
}
