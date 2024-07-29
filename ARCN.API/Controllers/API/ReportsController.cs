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
        public async ValueTask<ActionResult<Reports>> Post([FromBody] Reports model)
        {

            var result=await reportsService.AddReportsAsync(model);
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateReports/{key}")]
        public async ValueTask<ActionResult<Reports>> Put(int key, [FromBody] Reports Reports)
        {
            var result = await reportsService.UpdateReportsAsync(key,Reports);
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
        public async ValueTask<ActionResult<Reports>> Delete(int key)
        {
            var result = await reportsService.DeleteReportsAsync(key);
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
