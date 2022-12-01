using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockBooking.Core.Models;

namespace MockBooking.Core.Services.JWTTokenService
{
    public interface ITokenService
    {
        string GenerateJwtToken(LoginModel model, int id);
    }
}
