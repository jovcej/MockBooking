using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockBooking.Core.Services.ManagerService;
using MockBooking.Domain.Entities.Search;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MockBooking.Controllers
{
    [Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public readonly IManagerServices _managerService;

        public SearchController(IManagerServices managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<ActionResult<List<SearchRes>>> Search(SearchReq searchReq)
        {
            var search = await _managerService.Search(searchReq);
            return Ok(search);
        }
    }
}
