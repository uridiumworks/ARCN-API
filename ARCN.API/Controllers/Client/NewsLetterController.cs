using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.Client
{
    [Route("client")]
    [AllowAnonymous]
    public class NewsLetterController : ODataController
    {
        private readonly INewsLetterService newsLetterService;
        private readonly ILogger<NewsLetterController> logger;
        private readonly INewsLetterRepository newsLetterRepository;

        public NewsLetterController(INewsLetterService newsLetterService, ILogger<NewsLetterController> logger, INewsLetterRepository newsLetterRepository)
        {
            this.newsLetterService = newsLetterService;
            this.logger = logger;
            this.newsLetterRepository = newsLetterRepository;
        }
        [HttpGet("GetAllNewsLetter")]
        [EnableQuery]
        public async ValueTask<ActionResult<NewsLetter>> GetAllNewsLetter()
        {

            var result = newsLetterRepository.FindAll();

            return Ok(result);


        }

        [HttpGet("GetNewsLetterById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<NewsLetter>> GetNewsLetterById(int key)
        {
            var result = await newsLetterRepository.FindByIdAsync(key);
            return Ok(result);
        }

    }
}
