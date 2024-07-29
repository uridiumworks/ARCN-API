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
        [HttpPost("CreateCordinationReport")]
        public async ValueTask<ActionResult<CordinationReport>> Post([FromBody] CordinationReport model,CancellationToken cancellationToken)
        {

            var result=await cordinationReportService.AddCordinationReportAsync(model,cancellationToken);
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateCordinationReport/{key}")]
        public async ValueTask<ActionResult<CordinationReport>> Put(int key, [FromBody] CordinationReportDataModel CordinationReport)
        {
            var result = await cordinationReportService.UpdateCordinationReportAsync(key,CordinationReport);
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("Delete/{key}")]
        public async ValueTask<ActionResult<CordinationReport>> Delete(int key)
        {
            var result = await cordinationReportService.DeleteCordinationReportAsync(key);
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
