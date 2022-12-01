using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using MockBooking.Shared.Enums;

namespace MockBooking.Domain.Entities.CheckStatus
{
    public class CheckStatusRes
    {
        [Key]
        public int Id { get; set; }
        public BookingStatusEnum Status { get; set; }
    }
}
