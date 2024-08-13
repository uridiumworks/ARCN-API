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
    public class ExtensionController : ODataController
    {
        private readonly IExtensionService ExtensionService;
        private readonly ILogger<ExtensionController> logger;
        private readonly IExtensionRepository ExtensionRepository;

        public ExtensionController(IExtensionService ExtensionService, ILogger<ExtensionController> logger, IExtensionRepository ExtensionRepository)
        {
            this.ExtensionService = ExtensionService;
            this.logger = logger;
            this.ExtensionRepository = ExtensionRepository;
        }
        [HttpGet("GetAllExtension")]
        [EnableQuery]
        public async ValueTask<ActionResult<Extension>> GetAllExtension()
        {

            var result = ExtensionRepository.FindAll();

            return Ok(result);

        }

        [HttpGet("GetExtensionById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Extension>> GetExtensionById(int key)
        {
            var result = await ExtensionRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
