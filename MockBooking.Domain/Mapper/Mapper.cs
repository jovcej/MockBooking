using AutoMapper;
using MockBooking.Domain.DtoModels;
using MockBooking.Domain.Entities;

namespace MockBooking.Shared.Helpers
{
    public static class Mapper
    {
        //From Domain to DTO
        public static UserDto ToDto(this User domainModel)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });

            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<UserDto>(domainModel);
        }

        //From DTO to domain
        public static User ToDomain(this UserDto domainModel)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
            });

            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<User>(domainModel);
        }
    }
}
