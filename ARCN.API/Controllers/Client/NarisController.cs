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
    public class NarisController : ODataController
    {
        private readonly INarisService narisService;
        private readonly ILogger<NarisController> logger;
        private readonly INarisRepository narisRepository;

        public NarisController(INarisService narisService, ILogger<NarisController> logger, INarisRepository narisRepository)
        {
            this.narisService = narisService;
            this.logger = logger;
            this.narisRepository = narisRepository;
        }
        [HttpGet("GetAllNaris")]
        [EnableQuery]
        public async ValueTask<ActionResult<Naris>> GetAllNaris()
        {

            var result = narisRepository.FindAll().OrderBy(x => x.CreatedDate);
            return Ok(result);

        }

        [HttpGet("GetNarisById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Naris>> GetNarisById(int key)
        {
            var result = await narisRepository.FindByIdAsync(key);
            return Ok(result);
        }

    }
}
