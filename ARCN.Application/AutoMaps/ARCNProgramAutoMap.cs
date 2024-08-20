
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class ARCNProgramAutoMap : Profile
    {
        public ARCNProgramAutoMap()
        {
            CreateMap<ProgramDataModel, ARCNProgram>().ReverseMap();
        }
    }
}
