using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.Client
{
    [Route("client")]
    [AllowAnonymous]
    public class JournalsController : ODataController
    {
        private readonly IJournalsService journalsService;
        private readonly ILogger<JournalsController> logger;
        private readonly IJournalRepository journalRepository;

        public JournalsController(IJournalsService journalsService, ILogger<JournalsController> logger, IJournalRepository journalRepository)
        {
            this.journalsService = journalsService;
            this.logger = logger;
            this.journalRepository = journalRepository;
        }
        [HttpGet("GetJournals")]
        [EnableQuery]
        public async ValueTask<ActionResult<Journals>> GetJournals()
        {

            var result = journalRepository.FindAll();
            return Ok(result);


        }

        [HttpGet("GetJournalById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Journals>> GetJournalById(int key)
        {
            var result = await journalRepository.FindByIdAsync(key);
            if (result != null)
            {
                return Ok(result);

            }
            else
            {
                return BadRequest();
            }
        }

    }
}
