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
    public class ExtensionController : ODataController
    {
        private readonly IExtensionService ExtensionService;
        private readonly ILogger<ExtensionController> logger;

        public ExtensionController(IExtensionService ExtensionService, ILogger<ExtensionController> logger)
        {
            this.ExtensionService = ExtensionService;
            this.logger = logger;
        }
        [HttpPost("CreateExtension")]
        public async ValueTask<ActionResult<Extension>> Post([FromBody] Extension model,CancellationToken cancellationToken)
        {

            var result=await ExtensionService.AddExtensionAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateExtension/{key}")]
        public async ValueTask<ActionResult<Extension>> Put(int key, [FromBody] ExtensionDataModel Extension)
        {
            var result = await ExtensionService.UpdateExtensionAsync(key,Extension);
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
        public async ValueTask<ActionResult<Extension>> Delete(int key)
        {
            var result = await ExtensionService.DeleteExtensionAsync(key);
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
