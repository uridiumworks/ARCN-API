
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class ProjectAutoMap : Profile
    {
        public ProjectAutoMap()
        {
            CreateMap<ProjectDataModel, Project>().ReverseMap();
        }
    }
}
