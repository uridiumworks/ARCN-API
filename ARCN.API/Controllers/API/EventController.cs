using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.API
{
    [Route("api/[controller]")]
    public class EventController : ODataController
    {
        private readonly IEventService eventService;
        private readonly ILogger<EventController> logger;

        public EventController(IEventService eventService, ILogger<EventController> logger)
        {
            this.eventService = eventService;
            this.logger = logger;
        }
        [HttpPost("CreateEvent")]
        public async ValueTask<ActionResult<Event>> Post([FromBody] Event model,CancellationToken cancellationToken)
        {

            var result=await eventService.AddEventAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateEvent/{key}")]
        public async ValueTask<ActionResult<Event>> Put(int key, [FromBody] EventDataModel Event)
        {
            var result = await eventService.UpdateEventAsync(key,Event);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }
        }
        [HttpDelete("Delete/{key}")]
        public async ValueTask<ActionResult<Event>> Delete(int key)
        {
            var result = await eventService.DeleteEventAsync(key);
            if (result.Success)
            {
                return StatusCode(result.StatusCode);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }
        }

    }
}
