using AutoMapper;
using Projects.Domain.Commands.Inscription;
using Projects.Domain.Commands.Project;
using Projects.Domain.Commands.Task;
using Projects.Domain.DTO.Inscription;
using Projects.Domain.DTO.Project;
using Projects.Domain.DTO.Task;
using Projects.Domain.Entities;

namespace Projects.API.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            #region Project
            CreateMap<NewProjectCommand, Project>().ReverseMap();
            CreateMap<Project, NewProjectDTO>().ReverseMap();
            CreateMap<UpdateProjectCommand, Project>().ReverseMap();
            CreateMap<Project, UpdateProjectDTO>().ReverseMap();
            //use cases
            CreateMap<OpenProjectCommand, Project>().ReverseMap();
            #endregion

            #region Task
            CreateMap<NewTaskCommand, Domain.Entities.Task>().ReverseMap();
            CreateMap<Domain.Entities.Task, NewTaskDTO>().ReverseMap();
            CreateMap<UpdateTaskCommand, Domain.Entities.Task>().ReverseMap();
            CreateMap<Domain.Entities.Task, UpdateTaskDTO>().ReverseMap();
            #endregion

            #region Inscription
            CreateMap<NewInscriptionCommand, Inscription>().ReverseMap();
            CreateMap<Inscription, NewInscriptionDTO>().ReverseMap();
            CreateMap<Inscription, InscriptionRespondedDTO>().ReverseMap();
            //CreateMap<UpdateInscriptionCommand, Inscription>().ReverseMap();
            #endregion
        }
    }
}