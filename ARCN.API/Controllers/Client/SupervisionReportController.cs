using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.Client
{
    [Route("client")]
    [AllowAnonymous]
    public class SupervisionReportController : ODataController
    {
        private readonly ISupervisionReportService supervisionReportService;
        private readonly ILogger<SupervisionReportController> logger;
        private readonly ISupervisionReportRepository supervisionReportRepository;

        public SupervisionReportController(ISupervisionReportService supervisionReportService, ILogger<SupervisionReportController> logger, ISupervisionReportRepository supervisionReportRepository)
        {
            this.supervisionReportService = supervisionReportService;
            this.logger = logger;
            this.supervisionReportRepository = supervisionReportRepository;
        }
        [HttpGet("GetAllSupervisionReport")]
        [EnableQuery]
        public async ValueTask<ActionResult<SupervisionReport>> GetAllSupervisionReport()
        {

            var result = supervisionReportRepository.FindAll().OrderBy(x => x.CreatedDate);

            return Ok(result);

        }

        [HttpGet("GetSupervisionReportById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<SupervisionReport>> GetSupervisionReportById(int key)
        {
            var result = await supervisionReportRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
