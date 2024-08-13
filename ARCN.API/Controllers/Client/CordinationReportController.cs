using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using ARCN.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.Client
{
    [Route("client")]
    [AllowAnonymous]
    public class CordinationReportController : ODataController
    {
        private readonly ICordinationReportService cordinationReportService;
        private readonly ILogger<CordinationReportController> logger;
        private readonly ICordinationReportRepository cordinationReportRepository;

        public CordinationReportController(ICordinationReportService cordinationReportService, ILogger<CordinationReportController> logger
            , ICordinationReportRepository cordinationReportRepository)
        {
            this.cordinationReportService = cordinationReportService;
            this.logger = logger;
            this.cordinationReportRepository = cordinationReportRepository;
        }
        [HttpGet("GetAllCordinationReport")]
        [EnableQuery]
        public async ValueTask<ActionResult<CordinationReport>> GetAllCordinationReport()
        {

            var result = cordinationReportRepository.FindAll();
            return Ok(result);


        }

        [HttpGet("GetCordinationReportById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<CordinationReport>> GetCordinationReportById(int key)
        {
            var result = await cordinationReportRepository.FindByIdAsync(key);
            return Ok(result);
        }

    }
}
