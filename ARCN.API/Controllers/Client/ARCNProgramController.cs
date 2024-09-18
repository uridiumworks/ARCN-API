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
    public class ARCNProgramController : ODataController
    {
        private readonly IProgramService programService;
        private readonly ILogger<ARCNProgramController> logger;
        private readonly IProgramRepository programRepository;

        public ARCNProgramController(IProgramService programService, ILogger<ARCNProgramController> logger, IProgramRepository programRepository)
        {
            this.programService = programService;
            this.logger = logger;
            this.programRepository = programRepository;
        }
        [HttpGet("GetAllProgram")]
        [EnableQuery]
        public async ValueTask<ActionResult<ARCNProgram>> GetAllProgram()
        {

            var result = programRepository.FindAll().OrderBy(x=>x.CreatedDate);

            return Ok(result);

        }

        [HttpGet("GetProgramById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<ARCNProgram>> GetProgramById(int key)
        {
            var result = await programRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
