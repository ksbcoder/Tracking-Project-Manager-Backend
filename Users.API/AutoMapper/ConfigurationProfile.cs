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
            CreateMap<WorkTeam, NewWorkTeam>().ReverseMap();
            CreateMap<WorkTeamMongo, WorkTeam>().ReverseMap();
        }
    }
}
