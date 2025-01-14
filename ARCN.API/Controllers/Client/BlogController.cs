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

namespace ARCN.API.Controllers.Client
{
    [Route("client")]
    [AllowAnonymous]
    public class BlogController : ODataController
    {
        private readonly IBlogService blogService;
        private readonly ILogger<BlogController> logger;
        private readonly IBlogRepository blogRepository;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger, IBlogRepository blogRepository)
        {
            this.blogService = blogService;
            this.logger = logger;
            this.blogRepository = blogRepository;
        }
        [HttpGet("GetAllBlog")]
        [EnableQuery]
        public async ValueTask<ActionResult<Blog>> GetAllBlog()
        {

            var result = blogRepository.FindAll().OrderBy(x => x.CreatedDate); 

            return Ok(result);

        }

        [HttpGet("GetBlogById/{key}")]
        [EnableQuery]
        public async ValueTask<ActionResult<Blog>> GetBlogById(int key)
        {
            var result = await blogRepository.FindByIdAsync(key);

            return Ok(result);

        }

    }
}
