using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.ODATA
{
    [Route("odata")]
    [AllowAnonymous]
    public class JournalsController : ODataController
    {
        private readonly IJournalsService journalsService;
        private readonly ILogger<JournalsController> logger;

        public JournalsController(IJournalsService journalsService, ILogger<JournalsController> logger)
        {
            this.journalsService = journalsService;
            this.logger = logger;
        }
        [HttpGet("GetJournals")]
        [EnableQuery]
        public async ValueTask<ActionResult<Journals>> GetJournals()
        {

            var result = await journalsService.GetAllJournals();
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetJournalById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Journals>> GetJournalById(int key)
        {
            var result = await journalsService.GetJournalsById(key);
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }

    }
}
