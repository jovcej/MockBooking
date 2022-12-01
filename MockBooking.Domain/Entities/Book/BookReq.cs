using MockBooking.Domain.Entities.Search;
using System.ComponentModel.DataAnnotations;

namespace MockBooking.Domain.Entities.Book
{
    public class BookReq
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string OptionCode { get; set; }
        [Required]
        public SearchReq SearchReq { get; set; }
    }
}
