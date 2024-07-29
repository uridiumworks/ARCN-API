
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class ReportsAutoMap : Profile
    {
        public ReportsAutoMap()
        {
            CreateMap<ReportsDataModel, Reports>().ReverseMap();
        }
    }
}
