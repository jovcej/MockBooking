using MockBooking.Core.Models;
using MockBooking.Domain.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBooking.Core.Services.UserService
{
    public interface IUserService
    {
        Task<int> Create(UserDto userDtoModel);
        Task<JwtResponseModel> Login(LoginModel userDtoModel);
    }
}
