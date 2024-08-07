
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class EntrepreneurshipAutoMap : Profile
    {
        public EntrepreneurshipAutoMap()
        {
            CreateMap<EntrepreneurshipDataModel, Entrepreneurship>().ReverseMap();
        }
    }
}
