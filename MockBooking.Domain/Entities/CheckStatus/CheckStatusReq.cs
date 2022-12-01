using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.Domain.Entities.CheckStatus
{
    public class CheckStatusReq
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookingCode { get; set; }
    }
}
