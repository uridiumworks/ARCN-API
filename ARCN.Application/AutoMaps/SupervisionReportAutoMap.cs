
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class SupervisionReportAutoMap : Profile
    {
        public SupervisionReportAutoMap()
        {
            CreateMap<SupervisionReportDataModel, SupervisionReport>().ReverseMap();
        }
    }
}
