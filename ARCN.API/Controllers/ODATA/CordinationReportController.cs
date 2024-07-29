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
    public class CordinationReportController : ODataController
    {
        private readonly ICordinationReportService cordinationReportService;
        private readonly ILogger<CordinationReportController> logger;

        public CordinationReportController(ICordinationReportService cordinationReportService, ILogger<CordinationReportController> logger)
        {
            this.cordinationReportService = cordinationReportService;
            this.logger = logger;
        }
        [HttpGet("GetAllCordinationReport")]
        [EnableQuery]
        public async ValueTask<ActionResult<CordinationReport>> GetAllCordinationReport()
        {

            var result = await cordinationReportService.GetAllCordinationReport();
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetCordinationReportById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<CordinationReport>> GetCordinationReportById(int key)
        {
            var result = await cordinationReportService.GetCordinationReportById(key);
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
