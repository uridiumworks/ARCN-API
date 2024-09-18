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
    public class ProjectController : ODataController
    {
        private readonly IProjectService projectService;
        private readonly ILogger<ProjectController> logger;
        private readonly IProjectRepository ProjectRepository;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger, IProjectRepository ProjectRepository)
        {
            this.projectService = projectService;
            this.logger = logger;
            this.ProjectRepository = ProjectRepository;
        }
        [HttpGet("GetAllProject")]
        [EnableQuery]
        public async ValueTask<ActionResult<Project>> GetAllProject()
        {

            var result = ProjectRepository.FindAll().OrderBy(x => x.CreatedDate);

            return Ok(result);

        }

        [HttpGet("GetProjectById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Project>> GetProjectById(int key)
        {
            var result = await ProjectRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
