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
    public class ReportsController : ODataController
    {
        private readonly IReportsService reportsService;
        private readonly ILogger<ReportsController> logger;

        public ReportsController(IReportsService reportsService, ILogger<ReportsController> logger)
        {
            this.reportsService = reportsService;
            this.logger = logger;
        }
        [HttpGet("GetAllReports")]
        [EnableQuery]
        public async ValueTask<ActionResult<Reports>> GetAllReports()
        {

            var result = await reportsService.GetAllReports();
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetReportsById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Reports>> GetReportsById(int key)
        {
            var result = await reportsService.GetReportsById(key);
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
