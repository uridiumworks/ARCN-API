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
    public class SupervisionReportController : ODataController
    {
        private readonly ISupervisionReportService supervisionReportService;
        private readonly ILogger<SupervisionReportController> logger;
        private readonly ISupervisionReportRepository SupervisionReportRepository;

        public SupervisionReportController(ISupervisionReportService supervisionReportService, ILogger<SupervisionReportController> logger, ISupervisionReportRepository SupervisionReportRepository)
        {
            this.supervisionReportService = supervisionReportService;
            this.logger = logger;
            this.SupervisionReportRepository = SupervisionReportRepository;
        }
        [HttpGet("GetAllSupervisionReport")]
        [EnableQuery]
        public async ValueTask<ActionResult<SupervisionReport>> GetAllSupervisionReport()
        {

            var result = SupervisionReportRepository.FindAll();

            return Ok(result);

        }

        [HttpGet("GetSupervisionReportById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<SupervisionReport>> GetSupervisionReportById(int key)
        {
            var result = await SupervisionReportRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
