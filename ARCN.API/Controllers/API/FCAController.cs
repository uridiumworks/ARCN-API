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
    public class FCAController : ODataController
    {
        private readonly IFCAService FCAService;
        private readonly ILogger<FCAController> logger;

        public FCAController(IFCAService FCAService, ILogger<FCAController> logger)
        {
            this.FCAService = FCAService;
            this.logger = logger;
        }
        [HttpPost("CreateFCA")]
        public async ValueTask<ActionResult<FCA>> Post([FromBody] FCA model,CancellationToken cancellationToken)
        {

            var result=await FCAService.AddFCAAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateFCA/{key}")]
        public async ValueTask<ActionResult<FCA>> Put(int key, [FromBody] FCADataModel FCA)
        {
            var result = await FCAService.UpdateFCAAsync(key,FCA);
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
        public async ValueTask<ActionResult<FCA>> Delete(int key)
        {
            var result = await FCAService.DeleteFCAAsync(key);
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
