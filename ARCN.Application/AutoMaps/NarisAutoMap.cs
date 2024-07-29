
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class NarisAutoMap : Profile
    {
        public NarisAutoMap()
        {
            CreateMap<NarisDataModel, Naris>().ReverseMap();
        }
    }
}
