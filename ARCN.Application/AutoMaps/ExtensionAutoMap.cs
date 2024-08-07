
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class ExtensionAutoMap : Profile
    {
        public ExtensionAutoMap()
        {
            CreateMap<ExtensionDataModel, Extension>().ReverseMap();
        }
    }
}
