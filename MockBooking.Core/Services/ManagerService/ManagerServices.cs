using Microsoft.Extensions.Configuration;
using MockBooking.DataAccess.Repositories.ManagerRepo;
using MockBooking.Domain.DtoModels;
using MockBooking.Domain.Entities;
using MockBooking.Domain.Entities.Book;
using MockBooking.Domain.Entities.CheckStatus;
using MockBooking.Domain.Entities.Search;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MockBooking.Core.Services.ManagerService
{
    public class ManagerServices : IManagerServices
    {
        public readonly IManagerRepository _managerRepository;
        private readonly IConfiguration _configuration;

        public ManagerServices(IManagerRepository managerRepository, IConfiguration configuration)
        {
            _managerRepository = managerRepository;
            _configuration = configuration;
        }

        public async Task<BookRes> Book(BookReq bookReq)
        {
            var ret = await _managerRepository.Create(bookReq);
            return await Task.FromResult(ret);
        }

        public async Task<CheckStatusRes> CheckStatus(CheckStatusReq checkStatusReq)
        {
            var ret = await _managerRepository.GetStatus(checkStatusReq);
            return await Task.FromResult(ret);
        }

        public async Task<SearchRes> Search(SearchReq searchReq)
        {
            await RequiredField(searchReq);

            var search = new List<Option>();
            var fromDate = searchReq.FromDate;
            var lastminuteHotel = (fromDate - DateTime.Now).Days;
       

            if ((searchReq.DepartureAirport == "" || searchReq.DepartureAirport is null)  && lastminuteHotel > 45)
            {
                var hotelOnly = await _managerRepository.GetHotelOnly(searchReq);

                search.AddRange(hotelOnly);
            }
            else if ((searchReq.DepartureAirport != "" || searchReq.DepartureAirport is not null) && lastminuteHotel > 45)
            {
                var hotelAndFlight = await _managerRepository.GetHotelAndFlight(searchReq);

                search.AddRange(hotelAndFlight);
            }
            else if (lastminuteHotel <= 45)
            {
                var lastMinuteHotels = await _managerRepository.GetLastMinuteHotels(searchReq);
                search.AddRange(lastMinuteHotels);
            }

            var ret = new SearchRes()
            {
                Options = search
            };

            return await Task.FromResult(ret);
        }

        public async Task<bool> RequiredField(SearchReq searchReq)
        {
            Regex destination = new Regex(_configuration["RegexValidation:DestinationAndDeparture"]);
            Regex departure = new Regex(_configuration["RegexValidation:DestinationAndDeparture"]);


            //Regex dates = new Regex(_configuration["RegexValidation:Date"]);

            //if (!dates.IsMatch(searchReq.FromDate.ToString())) throw new Exception();
            //if (!dates.IsMatch(searchReq.ToDate.ToString())) throw new Exception();
                            
            if (!destination.IsMatch(searchReq.Destination.Trim())) throw new Exception();
            
            if (!String.IsNullOrEmpty(searchReq.DepartureAirport)) 
            {
                if (!departure.IsMatch(searchReq.DepartureAirport.Trim())) throw new Exception();
            }
                                
            return await Task.FromResult(true);
        }
    }
}
