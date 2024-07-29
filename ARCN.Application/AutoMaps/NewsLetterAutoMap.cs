
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class NewsLetterAutoMap : Profile
    {
        public NewsLetterAutoMap()
        {
            CreateMap<NewsLetterDataModel, NewsLetter>().ReverseMap();
        }
    }
}
