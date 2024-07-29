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
    public class BlogController : ODataController
    {
        private readonly IBlogService blogService;
        private readonly ILogger<BlogController> logger;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            this.blogService = blogService;
            this.logger = logger;
        }
        [HttpPost("CreateBlog")]
        public async ValueTask<ActionResult<Blog>> Post([FromBody] Blog model)
        {

            var result=await blogService.AddBlogAsync(model);
            if (result.Success)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateBlog/{key}")]
        public async ValueTask<ActionResult<Blog>> Put(int key, [FromBody] Blog blog)
        {
            var result = await blogService.UpdateBlogAsync(key,blog);
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
        public async ValueTask<ActionResult<Blog>> Delete(int key)
        {
            var result = await blogService.DeleteBlogAsync(key);
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
