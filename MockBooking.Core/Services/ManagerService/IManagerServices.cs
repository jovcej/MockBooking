using MockBooking.Domain.Entities.Book;
using MockBooking.Domain.Entities.CheckStatus;
using MockBooking.Domain.Entities.Search;

namespace MockBooking.Core.Services.ManagerService
{
    public interface IManagerServices
    {
        Task<SearchRes> Search(SearchReq searchReq);
        Task<BookRes> Book(BookReq bookReq);
        Task<CheckStatusRes> CheckStatus(CheckStatusReq checkStatusReq);
    }
}
