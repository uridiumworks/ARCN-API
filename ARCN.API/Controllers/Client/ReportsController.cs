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
    public class ReportsController : ODataController
    {
        private readonly IReportsService reportsService;
        private readonly ILogger<ReportsController> logger;
        private readonly IReportsRepository reportsRepository;

        public ReportsController(IReportsService reportsService, ILogger<ReportsController> logger, IReportsRepository reportsRepository)
        {
            this.reportsService = reportsService;
            this.logger = logger;
            this.reportsRepository = reportsRepository;
        }
        [HttpGet("GetAllReports")]
        [EnableQuery]
        public async ValueTask<ActionResult<Reports>> GetAllReports()
        {

            var result = reportsRepository.FindAll().OrderBy(x => x.CreatedDate);
            return Ok(result);

        }

        [HttpGet("GetReportsById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Reports>> GetReportsById(int key)
        {
            var result = await reportsRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
