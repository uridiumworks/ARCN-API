
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class FCAAutoMap : Profile
    {
        public FCAAutoMap()
        {
            CreateMap<FCADataModel, FCA>().ReverseMap();
        }
    }
}
