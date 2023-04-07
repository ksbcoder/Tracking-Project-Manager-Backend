using AutoMapper;
using Projects.Domain.Commands;
using Projects.Domain.DTO;
using Projects.Domain.Entities;

namespace Projects.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<NewProjectCommand, Project>().ReverseMap();
            CreateMap<Project, NewProjectDTO>().ReverseMap();

            CreateMap<UpdateProjectCommand, Project>().ReverseMap();
            CreateMap<Project, UpdateProjectDTO>().ReverseMap();

            //use cases
            CreateMap<OpenProjectCommand, Project>().ReverseMap();
        }
    }
}