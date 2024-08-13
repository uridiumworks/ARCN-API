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
    public class ARCNProgramController : ODataController
    {
        private readonly IProgramService programService;
        private readonly ILogger<ARCNProgramController> logger;

        public ARCNProgramController(IProgramService programService, ILogger<ARCNProgramController> logger)
        {
            this.programService = programService;
            this.logger = logger;
        }
        [HttpPost("CreateProgram")]
        public async ValueTask<ActionResult<ARCNProgram>> Post([FromBody] ARCNProgram model,CancellationToken cancellationToken)
        {

            var result=await programService.AddProgramAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateProgram/{key}")]
        public async ValueTask<ActionResult<ARCNProgram>> Put(int key, [FromBody] ProgramDataModel Program)
        {
            var result = await programService.UpdateProgramAsync(key,Program);
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
        public async ValueTask<ActionResult<ARCNProgram>> Delete(int key)
        {
            var result = await programService.DeleteProgramAsync(key);
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
