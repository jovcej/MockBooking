using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockBooking.Domain.Entities;

namespace MockBooking.DataAccess.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> CreateUser(User userModel)
        {
            var userExist = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userModel.Email);
            if (userExist != null) throw new Exception();

      
                var user = new User()
                {
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Password = userModel.Password,
                };

                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return user;

            //throw new Exception();
           
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }



    }
}
