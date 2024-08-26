using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Repository.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class EventService:IEventService
    {
        private readonly IEventRepository EventRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public EventService(
            IEventRepository EventRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,
            IMapper mapper) {
            this.EventRepository = EventRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Event>> AddEventAsync(Event model, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Event>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode = 404
                    };
                }
                model.UserProfileId = user.Id;
                var result= await EventRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<Event> 
                {
                    Success = true,
                    Message = "Successfully submitted", 
                    Data = result,
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Event>
                {
                    Success = false,
                    Message =ex.Message,
                    StatusCode = 500
                };
            }
        }
        public async ValueTask<ResponseModel<Event>> GetAllEvent()
        {
            var Events =  EventRepository.FindAll();

            if (Events == null)
                return ResponseModel<Event>.ErrorMessage("Events not found");

            return ResponseModel<Event>.SuccessMessage(data: Events);
        }
        public async ValueTask<ResponseModel<Event>> GetEventById(int Eventid)
        {
            var Events = await EventRepository.FindByIdAsync(Eventid);

            if (Events == null)
                return ResponseModel<Event>.ErrorMessage("Events not found");

            return ResponseModel<Event>.SuccessMessage(data: Events);
        }
        public double GetAllEventTotal()
        {
            var events = EventRepository.FindAll().Where(x => x.CreatedDate < DateTime.Now.Date.AddMonths(-1)).Count();
            return events;
        }
        public double GetAllEventPreviousTotal()
        {
            var events = EventRepository.FindAll().Where(x => x.CreatedDate > DateTime.Now.Date.AddMonths(-1)).Count();
            return events;
        }
        public async ValueTask<ResponseModel<Event>> UpdateEventAsync(int Eventid, EventDataModel model)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Event>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode=404
                    };
                }
                var Events = await EventRepository.FindByIdAsync(Eventid);
                if (Events != null)
                {
                    mapper.Map(model, Events);

                    var res= EventRepository.Update(Events);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Event>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseModel<Event>
                    {
                        Success = false,
                        Message = "Update Failed",
                        StatusCode = 404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Event>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteEventAsync(int Eventid)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode = 404
                    };
                }
                var Events = await EventRepository.FindByIdAsync(Eventid);
                if (Events != null)
                {
                    EventRepository.Remove(Events);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Event Deleted  successfully",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Failed to delete",
                        StatusCode = 404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
