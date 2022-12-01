using System.ComponentModel.DataAnnotations;

namespace MockBooking.Domain.Entities.Search
{
    public class SearchRes
    {
        //[Key]
        //public int Id { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
    }
    //public class ListaOdLetoviiHoteli {
    //    public List<string> HotelCodes { get; set; } = new();
    //    public List<string> FlightCodes { get; set; } = new();

    //}
}