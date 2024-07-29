
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class CordinationReportAutoMap : Profile
    {
        public CordinationReportAutoMap()
        {
            CreateMap<CordinationReportDataModel, CordinationReport>().ReverseMap();
        }
    }
}
