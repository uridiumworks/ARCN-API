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
    public class EntrepreneurshipController : ODataController
    {
        private readonly IEntrepreneurshipService entrepreneurshipService;
        private readonly ILogger<EntrepreneurshipController> logger;

        public EntrepreneurshipController(IEntrepreneurshipService entrepreneurshipService, ILogger<EntrepreneurshipController> logger)
        {
            this.entrepreneurshipService = entrepreneurshipService;
            this.logger = logger;
        }
        [HttpPost("CreateEntrepreneurship")]
        public async ValueTask<ActionResult<Entrepreneurship>> Post([FromBody] Entrepreneurship model,CancellationToken cancellationToken)
        {

            var result=await entrepreneurshipService.AddEntrepreneurshipAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateEntrepreneurship/{key}")]
        public async ValueTask<ActionResult<Entrepreneurship>> Put(int key, [FromBody] EntrepreneurshipDataModel Entrepreneurship)
        {
            var result = await entrepreneurshipService.UpdateEntrepreneurshipAsync(key,Entrepreneurship);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result); ;

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }
        }
        [HttpDelete("Delete/{key}")]
        public async ValueTask<ActionResult<Entrepreneurship>> Delete(int key)
        {
            var result = await entrepreneurshipService.DeleteEntrepreneurshipAsync(key);
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
