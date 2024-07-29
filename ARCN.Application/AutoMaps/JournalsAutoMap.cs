
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class JournalsAutoMap : Profile
    {
        public JournalsAutoMap()
        {
            CreateMap<JournalsDataModel, Journals>().ReverseMap();
        }
    }
}
