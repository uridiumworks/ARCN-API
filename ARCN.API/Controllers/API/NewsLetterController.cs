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
    public class NewsLetterController : ODataController
    {
        private readonly INewsLetterService newsLetterService;
        private readonly ILogger<NewsLetterController> logger;

        public NewsLetterController(INewsLetterService newsLetterService, ILogger<NewsLetterController> logger)
        {
            this.newsLetterService = newsLetterService;
            this.logger = logger;
        }
        [HttpPost("CreateNewsLetter")]
        public async ValueTask<ActionResult<NewsLetter>> Post([FromBody] NewsLetter model,CancellationToken cancellationToken)
        {

            var result=await newsLetterService.AddNewsLetterAsync(model, cancellationToken);
            if (result.Success)
            {
                return Ok(result);

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateNewsLetter/{key}")]
        public async ValueTask<ActionResult<NewsLetter>> Put(int key, [FromBody] NewsLetterDataModel NewsLetter)
        {
            var result = await newsLetterService.UpdateNewsLetterAsync(key,NewsLetter);
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
        public async ValueTask<ActionResult<NewsLetter>> Delete(int key)
        {
            var result = await newsLetterService.DeleteNewsLetterAsync(key);
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
