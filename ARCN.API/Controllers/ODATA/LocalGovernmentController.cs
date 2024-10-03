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
    public class LocalGovernmentController : ODataController
    {

        private readonly ILogger<LocalGovernmentController> logger;
        private readonly ILocalGovernmentAreaRepository LocalGovernmentRepository;

        public LocalGovernmentController(ILogger<LocalGovernmentController> logger, ILocalGovernmentAreaRepository LocalGovernmentRepository)
        {
            this.logger = logger;
            this.LocalGovernmentRepository = LocalGovernmentRepository;
        }

        [HttpGet("GetLocalGovernmentByStateId/{stateid}")]
        [EnableQuery]
        public async ValueTask<ActionResult<LocalGovernmentArea>> GetLocalGovernmentById(int stateid)
        {
            var result = LocalGovernmentRepository.FindAll().Where(x => x.StateId == stateid);
            return Ok(result);

        }

    }
}
