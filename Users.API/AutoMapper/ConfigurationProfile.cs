using AutoMapper;
using Users.Domain.Commands;
using Users.Domain.DTO;
using Users.Domain.Entities;
using Users.Infrastructure.Entities;

namespace Users.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<NewUserCommand, User>().ReverseMap();
            CreateMap<UpdateUserCommand, User>().ReverseMap();
            CreateMap<User, UserMongo>().ReverseMap();
            CreateMap<UserMongo, NewUserDTO>().ReverseMap();
            CreateMap<UserMongo, UpdateUserDTO>().ReverseMap();
        }
    }
}