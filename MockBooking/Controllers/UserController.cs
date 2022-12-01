using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockBooking.Core.Models;
using MockBooking.Core.Services.UserService;
using MockBooking.Domain.DtoModels;

namespace MockBooking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[AllowAnonymous]
		[HttpPost("registeruser")]
		public async Task<IActionResult> RegisterUser([FromBody] UserDto userEntity)
		{
			int userId = await _userService.Create(userEntity);
			return Ok(userId);
		}

		[AllowAnonymous]
		[HttpPost("loginuser")]
		public async Task<IActionResult> LoginUser([FromBody] LoginModel loginModel)
		{
			var jwt = await _userService.Login(loginModel);
			return Ok(jwt);
		}
	}
}

