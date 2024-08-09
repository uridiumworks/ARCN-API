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
    public class SupervisionReportController : ODataController
    {
        private readonly ISupervisionReportService supervisionReportService;
        private readonly ILogger<SupervisionReportController> logger;

        public SupervisionReportController(ISupervisionReportService supervisionReportService, ILogger<SupervisionReportController> logger)
        {
            this.supervisionReportService = supervisionReportService;
            this.logger = logger;
        }
        [HttpPost("CreateSupervisionReport")]
        public async ValueTask<ActionResult<SupervisionReport>> Post([FromBody] SupervisionReport model,CancellationToken cancellationToken)
        {

            var result=await supervisionReportService.AddSupervisionReportAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateSupervisionReport/{key}")]
        public async ValueTask<ActionResult<SupervisionReport>> Put(int key, [FromBody] SupervisionReportDataModel SupervisionReport)
        {
            var result = await supervisionReportService.UpdateSupervisionReportAsync(key,SupervisionReport);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }
        }
        [HttpDelete("Delete/{key}")]
        public async ValueTask<ActionResult<SupervisionReport>> Delete(int key)
        {
            var result = await supervisionReportService.DeleteSupervisionReportAsync(key);
            if (result.Success)
            {
                return StatusCode(result.StatusCode);
            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }
        }

    }
}
