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

namespace ARCN.API.Controllers.ODATA
{
    [Route("odata")]
    [AllowAnonymous]
    public class StateController : ODataController
    {

        private readonly ILogger<StateController> logger;
        private readonly IStateRepository StateRepository;

        public StateController(ILogger<StateController> logger,IStateRepository StateRepository)
        {
            this.logger = logger;
            this.StateRepository = StateRepository;
        }

        [HttpGet("GetState")]
        [EnableQuery]
        public async ValueTask<ActionResult<State>> GetState()
        {
            var result = StateRepository.FindAll();
            return Ok(result);  

        }

    }
}
