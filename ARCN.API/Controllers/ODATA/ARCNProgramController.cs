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

namespace ARCN.API.Controllers.ODATA
{
    [Route("odata")]
    [AllowAnonymous]
    public class ARCNProgramController : ODataController
    {
        private readonly IProgramService programService;
        private readonly ILogger<ARCNProgramController> logger;
        private readonly IProgramRepository ProgramRepository;

        public ARCNProgramController(IProgramService programService, ILogger<ARCNProgramController> logger,IProgramRepository ProgramRepository)
        {
            this.programService = programService;
            this.logger = logger;
            this.ProgramRepository = ProgramRepository;
        }
        [HttpGet("GetAllProgram")]
        [EnableQuery]
        public async ValueTask<ActionResult<ARCNProgram>> GetAllProgram()
        {

            var result = ProgramRepository.FindAll();

            return Ok(result);

        }

        [HttpGet("GetProgramById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<ARCNProgram>> GetProgramById(int key)
        {
            var result = await ProgramRepository.FindByIdAsync(key);

           return Ok(result);

        }

    }
}
