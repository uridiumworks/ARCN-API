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
    public class ReportsController : ODataController
    {
        private readonly IReportsService reportsService;
        private readonly ILogger<ReportsController> logger;

        public ReportsController(IReportsService reportsService, ILogger<ReportsController> logger)
        {
            this.reportsService = reportsService;
            this.logger = logger;
        }
        [HttpPost("CreateReports")]
        public async ValueTask<ActionResult<Reports>> Post([FromBody] Reports model,CancellationToken cancellationToken)
        {

            var result=await reportsService.AddReportsAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateReports/{key}")]
        public async ValueTask<ActionResult<Reports>> Put(int key, [FromBody] ReportsDataModel Reports)
        {
            var result = await reportsService.UpdateReportsAsync(key,Reports);
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
        public async ValueTask<ActionResult<Reports>> Delete(int key)
        {
            var result = await reportsService.DeleteReportsAsync(key);
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
