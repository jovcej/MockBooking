using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockBooking.Core.Services.ManagerService;
using MockBooking.Domain.Entities.CheckStatus;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MockBooking.Controllers
{
    [Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class CheckStatusController : ControllerBase
    {
        public readonly IManagerServices _managerService;

        public CheckStatusController(IManagerServices managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<ActionResult<List<CheckStatusRes>>> CheckStatus(CheckStatusReq checkstatusReq)
        {
            var checkStatus = await _managerService.CheckStatus(checkstatusReq);
            return Ok(checkStatus);
        }

    }
}
