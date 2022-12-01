using Microsoft.Extensions.Configuration;
using MockBooking.Core.Models;
using MockBooking.Core.Services.JWTTokenService;
using MockBooking.DataAccess.Repositories.UserRepo;
using MockBooking.Domain.DtoModels;
using MockBooking.Domain.Entities;
using MockBooking.Shared.Helpers;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace MockBooking.Core.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<int> Create(UserDto userDtoModel)
        {
            await RequiredField(userDtoModel);

            userDtoModel.Password = Methods.Sha512Hash(userDtoModel.Password);

            var user = await _userRepository.CreateUser(userDtoModel.ToDomain());

            return user.Id;
        }

        public async Task<JwtResponseModel> Login(LoginModel model)
        {
            User user = await _userRepository.GetUser(model.Email, Methods.Sha512Hash(model.Password));
            if (user is null) throw new UnauthorizedAccessException();

            var token = _tokenService.GenerateJwtToken(model, user.Id);
            JwtResponseModel jwtToken = new() { Jwt = token };
            return jwtToken;
        }

        public async Task<bool> RequiredField(UserDto model)
        {
            Regex emailRegex = new Regex(_configuration["RegexValidation:EmailRegex"]);
            Regex passwordRegex = new Regex(_configuration["RegexValidation:PasswordRegex"]);

            User currentUser = await _userRepository.GetUserByEmail(model.Email);

            if (currentUser != null) throw new Exception();

            if (!emailRegex.IsMatch(model.Email.Trim()))  throw new Exception();

            if (!passwordRegex.IsMatch(model.Password.Trim()))  throw new Exception();

            return await Task.FromResult(true);
        }
    }
}
