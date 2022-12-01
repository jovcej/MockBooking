using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockBooking.Core.Services.ManagerService;
using MockBooking.Domain.Entities.Book;
using MockBooking.Domain.Entities.Search;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MockBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IManagerServices _managerService;

        public BookController(IManagerServices managerService)
        {        
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<ActionResult<List<SearchRes>>> Book(BookReq bookReq)
        {
            var book = await _managerService.Book(bookReq);

            return Ok(book);
        }

    }
}
