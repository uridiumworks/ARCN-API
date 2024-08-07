
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class BlogAutoMap : Profile
    {
        public BlogAutoMap()
        {
            CreateMap<BlogDataModel, Blog>().ReverseMap();
        }
    }
}
