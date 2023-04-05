using AutoMapper;
using Users.Domain.Commands;
using Users.Domain.Entities;
using Users.Infrastructure.Entities;

namespace Users.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<User, NewUser>().ReverseMap();
            CreateMap<UserMongo, User>().ReverseMap();
            CreateMap<UserMongo, NewUser>().ReverseMap();
        }
    }
}
