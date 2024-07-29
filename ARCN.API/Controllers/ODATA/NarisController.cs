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
    public class NarisController : ODataController
    {
        private readonly INarisService narisService;
        private readonly ILogger<NarisController> logger;

        public NarisController(INarisService narisService, ILogger<NarisController> logger)
        {
            this.narisService = narisService;
            this.logger = logger;
        }
        [HttpGet("GetAllNaris")]
        [EnableQuery]
        public async ValueTask<ActionResult<Naris>> GetAllNaris()
        {

            var result = await narisService.GetAllNaris();
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetNarisById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Naris>> GetNarisById(int key)
        {
            var result = await narisService.GetNarisById(key);
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
