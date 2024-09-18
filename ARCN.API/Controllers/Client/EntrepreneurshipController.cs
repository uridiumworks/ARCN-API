using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Internal;

namespace ARCN.API.Controllers.Client
{
    [Route("client")]
    [AllowAnonymous]
    public class EntrepreneurshipController : ODataController
    {
        private readonly IEntrepreneurshipService entrepreneurshipService;
        private readonly ILogger<EntrepreneurshipController> logger;
        private readonly IEntrepreneurshipRepository EntrepreneurshipRepository;

        public EntrepreneurshipController(IEntrepreneurshipService entrepreneurshipService, ILogger<EntrepreneurshipController> logger, IEntrepreneurshipRepository EntrepreneurshipRepository)
        {
            this.entrepreneurshipService = entrepreneurshipService;
            this.logger = logger;
            this.EntrepreneurshipRepository = EntrepreneurshipRepository;
        }
        [HttpGet("GetAllEntrepreneurship")]
        [EnableQuery]
        public async ValueTask<ActionResult<Entrepreneurship>> GetAllEntrepreneurship()
        {

            var result = EntrepreneurshipRepository.FindAll().OrderBy(x => x.CreatedDate);

            return Ok(result);

        }

        [HttpGet("GetEntrepreneurshipById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Entrepreneurship>> GetEntrepreneurshipById(int key)
        {
            var result = await EntrepreneurshipRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
