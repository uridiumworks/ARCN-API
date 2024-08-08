using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IEventService
    {
        ValueTask<ResponseModel<Event>> AddEventAsync(Event model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Event>> GetAllEvent();
        ValueTask<ResponseModel<Event>> GetEventById(int Eventid);
        ValueTask<ResponseModel<Event>> UpdateEventAsync(int Eventid, EventDataModel model);
        ValueTask<ResponseModel<string>> DeleteEventAsync(int Eventid);
        double GetAllEventPreviousTotal();
        double GetAllEventTotal();
    }
}
