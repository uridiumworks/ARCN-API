﻿using ARCN.Application.DataModels.Identity;
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
    public class FCAController : ODataController
    {
        private readonly IFCAService FCAService;
        private readonly ILogger<FCAController> logger;
        private readonly IFCARepository FCARepository;

        public FCAController(IFCAService FCAService, ILogger<FCAController> logger,IFCARepository FCARepository)
        {
            this.FCAService = FCAService;
            this.logger = logger;
            this.FCARepository = FCARepository;
        }
        [HttpGet("GetAllFCA")]
        [EnableQuery]
        public async ValueTask<ActionResult<FCA>> GetAllFCA()
        {

            var result = FCARepository.FindAll();

            return Ok(result);

        }

        [HttpGet("GetFCAById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<FCA>> GetFCAById(int key)
        {
            var result = await FCARepository.FindByIdAsync(key);

           return Ok(result);

        }

    }
}
