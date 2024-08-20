
using ARCN.Application.DataModels.ContentManagement;
using AutoMapper;

namespace ARCN.Application.AutoMaps
{
    public class EventAutoMap : Profile
    {
        public EventAutoMap()
        {
            CreateMap<EventDataModel, Event>().ReverseMap();
        }
    }
}
