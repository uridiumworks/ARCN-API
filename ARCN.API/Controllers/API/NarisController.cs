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
    public class NarisController : ODataController
    {
        private readonly INarisService narisService;
        private readonly ILogger<NarisController> logger;

        public NarisController(INarisService narisService, ILogger<NarisController> logger)
        {
            this.narisService = narisService;
            this.logger = logger;
        }
        [HttpPost("CreateNaris")]
        public async ValueTask<ActionResult<Naris>> Post([FromBody] Naris model)
        {

            var result=await narisService.AddNarisAsync(model);
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateNaris/{key}")]
        public async ValueTask<ActionResult<Naris>> Put(int key, [FromBody] Naris Naris)
        {
            var result = await narisService.UpdateNarisAsync(key,Naris);
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
        public async ValueTask<ActionResult<Naris>> Delete(int key)
        {
            var result = await narisService.DeleteNarisAsync(key);
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
