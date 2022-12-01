using MockBooking.Domain.Entities.Book;
using MockBooking.Domain.Entities.CheckStatus;
using MockBooking.Domain.Entities.Search;

namespace MockBooking.DataAccess.Repositories.ManagerRepo
{
    public interface IManagerRepository
    {
        Task<List<Option>> GetHotelOnly(SearchReq searchReq);
        Task<List<Option>> GetHotelAndFlight(SearchReq searchReq);
        Task<List<Option>> GetLastMinuteHotels(SearchReq searchReq);
        Task<BookRes> Create(BookReq bookReq);
        Task<CheckStatusRes> GetStatus(CheckStatusReq checkStatusReq);
    }
}
