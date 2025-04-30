using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;



using Business.Dtos;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace Business.MapperProfiles;
//Got help from Chat-GPT4o to set up AutoMapper, for cleaner code and avoiding having to map each property manually
public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectDto, ProjectEntity>();

        CreateMap<ProjectEntity, ProjectDto>()
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.ClientName : ""));

        CreateMap<UpdateProjectDto, ProjectEntity>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
