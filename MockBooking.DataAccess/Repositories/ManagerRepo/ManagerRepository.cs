using MockBooking.DataAccess.Models;
using MockBooking.Domain.Entities;
using MockBooking.Domain.Entities.Book;
using MockBooking.Domain.Entities.CheckStatus;
using MockBooking.Domain.Entities.Search;
using MockBooking.Shared.Enums;

using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MockBooking.DataAccess.Repositories.ManagerRepo
{
    public class ManagerRepository : IManagerRepository
    {

        private readonly DataContext _context;

        public ManagerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Option>> GetHotelAndFlight(SearchReq searchReq)
        {
            string jsonHotels;
            string jsonFlights;

            using (var client = new HttpClient())
            {
                var hotelsUrl = $"https://tripx-test-functions.azurewebsites.net/api/SearchHotels?destinationCode={searchReq.Destination}";
                var FlighstUrl = $"https://tripx-test-functions.azurewebsites.net/api/SearchFlights?departureAirport={searchReq.DepartureAirport}" +
                                 $"&arrivalAirport={searchReq.Destination}";

                client.DefaultRequestHeaders.Add("destinationCode", searchReq.Destination);
                client.DefaultRequestHeaders.Add("departureAirport", searchReq.DepartureAirport);

                var hotelsEndpoint = new Uri(hotelsUrl);
                var flightsEndpoint = new Uri(FlighstUrl);

                var hotelResult = await client.GetAsync(hotelsEndpoint);
                var flightsResult = await client.GetAsync(flightsEndpoint);

                //serialize
                jsonHotels = await hotelResult.Content.ReadAsStringAsync();
                jsonFlights = await flightsResult.Content.ReadAsStringAsync();

            }

            //deserialize
            var responseHotels = JsonConvert.DeserializeObject<List<HotelResponse>>(jsonHotels);
            var responseFlights = JsonConvert.DeserializeObject<List<FlightResponse>>(jsonFlights);


            List<Option> options = new();
            foreach (var hotel in responseHotels)
            {
                foreach (var flight in responseFlights)
                {
                    Option option = new()
                    {
                        FlightCode = flight.FlightCode.ToString(),
                        HotelCode = hotel.HotelCode.ToString(),
                        ArrivalAirport = flight.ArrivalAirport,
                        OptionCode = Guid.NewGuid().ToString()
                    };
                    options.Add(option);

                    await _context.AddAsync(option);
                    _context.SaveChanges();
                }
            }

            return options;
        }

        public async Task<List<Option>> GetHotelOnly(SearchReq searchReq)
        {
            string jsonHotels;

            using (var client = new HttpClient())
            {
                var hotelsUrl = $"https://tripx-test-functions.azurewebsites.net/api/SearchHotels?destinationCode={searchReq.Destination}";
                client.DefaultRequestHeaders.Add("destinationCode", searchReq.Destination);

                var hotelsEdpoint = new Uri(hotelsUrl);

                var result = await client.GetAsync(hotelsEdpoint);
                jsonHotels = await result.Content.ReadAsStringAsync();

            }

            var ret = JsonConvert.DeserializeObject<List<Option>>(jsonHotels);

            ret.ForEach(async (x) =>
            {

                await _context.AddAsync(x);
                _context.SaveChanges();

            });


            return ret;
        }

        public async Task<List<Option>> GetLastMinuteHotels(SearchReq searchReq)
        {
            string jsonLasHotels;

            using (var client = new HttpClient())
            {
                var url = $"https://tripx-test-functions.azurewebsites.net/api/SearchHotels?destinationCode={searchReq.Destination}";
                client.DefaultRequestHeaders.Add("destinationCode", searchReq.Destination);

                var lastHotels = new Uri(url);

                var result = await client.GetAsync(lastHotels);
                jsonLasHotels = await result.Content.ReadAsStringAsync();

            }

            var ret = JsonConvert.DeserializeObject<List<Option>>(jsonLasHotels);

            ret.ForEach(async (x) =>
            {

                await _context.AddAsync(x);
                _context.SaveChanges();

            });

            return ret;
        }

        public async Task<BookRes> Create(BookReq bookReq)
        {

            Random random = new Random();

            int sleepTime = random.Next(30, 60);
            var bookingTime = DateTime.Now;
            var bookingCode = RandomString(6);

            var ret = new BookRes
            {
                BookingTime = bookingTime,
                BookingCode = bookingCode,
            };

            BookedReservation bookedEntity = new()
            {
                Request = bookReq,
                Response = ret,
                SleepTime = sleepTime
            };

            await _context.AddAsync(bookedEntity);
            _context.SaveChanges();

            return ret;
        }

        public async Task<CheckStatusRes> GetStatus(CheckStatusReq checkStatusReq)
        {
            //hotel and flights
            var hotels = _context.Options.Select(x => x.HotelCode).ToList();        
            var flights = _context.Options.Select(x => x.FlightCode).ToList();

            //booking
            var sleeptTime = _context.BookRes.Select(x => x.SleepTime).FirstOrDefault();
            var bookingTime = _context.BookRes.Select(x => x.Response.BookingTime).FirstOrDefault();
            var time = bookingTime.AddSeconds(sleeptTime);

            //lastminuteHotels
            var fromDate = _context.BookRes.Select(x => x.Request.SearchReq.FromDate).FirstOrDefault();
            var lastminuteHotels = (fromDate - DateTime.Now).Days;


            BookingStatusEnum status = 0;
            if (hotels.TrueForAll(x => x != "") && flights.TrueForAll(x => x != "") && lastminuteHotels > 45)
            {
                if (DateTime.Now > time)
                {
                    status = BookingStatusEnum.Success;
                }
                else
                {
                    status = BookingStatusEnum.Pending;
                }

            }
            else if (lastminuteHotels <= 45)
            {
                if (DateTime.Now > time)
                {
                    status = BookingStatusEnum.Failed;
                }
                else
                {
                    status = BookingStatusEnum.Pending;
                }
            }

            var checkStatusRes = new CheckStatusRes
            {
                Status = status
            };

            await _context.AddAsync(checkStatusRes);
            _context.SaveChanges();

            return checkStatusRes;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
