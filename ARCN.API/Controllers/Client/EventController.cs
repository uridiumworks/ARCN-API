using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Internal;

namespace ARCN.API.Controllers.Client
{
    [Route("client")]
    [AllowAnonymous]
    public class EventController : ODataController
    {
        private readonly IEventService EventService;
        private readonly ILogger<EventController> logger;
        private readonly IEventRepository EventRepository;

        public EventController(IEventService EventService, ILogger<EventController> logger, IEventRepository EventRepository)
        {
            this.EventService = EventService;
            this.logger = logger;
            this.EventRepository = EventRepository;
        }
        [HttpGet("GetAllEvent")]
        [EnableQuery]
        public async ValueTask<ActionResult<Event>> GetAllEvent()
        {

            var result = EventRepository.FindAll();

            return Ok(result);

        }

        [HttpGet("GetEventById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Event>> GetEventById(int key)
        {
            var result = await EventRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
