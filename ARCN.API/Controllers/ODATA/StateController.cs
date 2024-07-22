using ARCN.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.ODATA
{

    /// <summary>
    /// State Controller
    /// </summary>
    [Route("odata")]
    public class StateController : ODataController
    {
        private readonly IStateRepository stateRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stateRepository"></param>
        public StateController(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }



        /// <summary>
        /// Get All State
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetStates")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(List<State>))]
        public async ValueTask<ActionResult<State>> GetStates()
        {
            var states = await stateRepository.GetStates();
            return Ok(states);
        }


    }
}
