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
    public class BlogController : ODataController
    {
        private readonly IBlogService blogService;
        private readonly ILogger<BlogController> logger;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            this.blogService = blogService;
            this.logger = logger;
        }
        [HttpGet("GetAllBlog")]
        [EnableQuery]
        public async ValueTask<ActionResult<Blog>> GetAllBlog()
        {

            var result = await blogService.GetAllBlog();
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetBlogById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Blog>> GetBlogById(int key)
        {
            var result = await blogService.GetBlogById(key);
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
