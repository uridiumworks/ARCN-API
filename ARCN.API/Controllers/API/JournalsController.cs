﻿using ARCN.Application.DataModels.ContentManagement;
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
    public class JournalsController : ODataController
    {
        private readonly IJournalsService journalsService;
        private readonly ILogger<JournalsController> logger;

        public JournalsController(IJournalsService journalsService, ILogger<JournalsController> logger)
        {
            this.journalsService = journalsService;
            this.logger = logger;
        }
        [HttpPost("CreateJournals")]
        public async ValueTask<ActionResult<Journals>> Post([FromBody] Journals model,CancellationToken cancellationToken)
        {

            var result=await journalsService.AddJournalsAsync(model, cancellationToken);
            if (result.Success)
            {
                return StatusCode(result.StatusCode,result);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }

        }

        [HttpPut("UpdateJournals/{key}")]
        public async ValueTask<ActionResult<Journals>> Put(int key, [FromBody] JournalsDataModel Journals)
        {
            var result = await journalsService.UpdateJournalsAsync(key,Journals);
            if (result.Success)
            {
                return StatusCode(result.StatusCode,result);

            }
            else
            {
                return StatusCode(result.StatusCode,result);
            }
        }
        [HttpDelete("Delete/{key}")]
        public async ValueTask<ActionResult<Journals>> Delete(int key)
        {
            var result = await journalsService.DeleteJournalsAsync(key);
            if (result.Success)
            {
                return StatusCode(result.StatusCode);

            }
            else
            {
                return StatusCode(result.StatusCode, result);
            }
        }
        
    }
}
