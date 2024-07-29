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
    public class NewsLetterController : ODataController
    {
        private readonly INewsLetterService newsLetterService;
        private readonly ILogger<NewsLetterController> logger;

        public NewsLetterController(INewsLetterService newsLetterService, ILogger<NewsLetterController> logger)
        {
            this.newsLetterService = newsLetterService;
            this.logger = logger;
        }
        [HttpGet("GetAllNewsLetter")]
        [EnableQuery]
        public async ValueTask<ActionResult<NewsLetter>> GetAllNewsLetter()
        {

            var result = await newsLetterService.GetAllNewsLetter();
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetNewsLetterById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<NewsLetter>> GetNewsLetterById(int key)
        {
            var result = await newsLetterService.GetNewsLetterById(key);
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
