using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.ODATA
{
    /// <summary>
    /// User Profile Controller
    /// </summary>
    [Route("odata")]
    public class UserController : ODataController
    {
        private readonly IUserprofileService userprofileService;
        private readonly IUserProfileRepository userProfileRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userRepository"></param>
        public UserController(IUserprofileService userprofileService, IUserProfileRepository userProfileRepository)
        {
            this.userprofileService = userprofileService;
            this.userProfileRepository = userProfileRepository;
        }



        /// <summary>
        /// Get All User Profile
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("User")]
        // [EnableQuery]
        public ActionResult<List<ApplicationUser>> GetAll()
        {
            var result = userProfileRepository.FindAll().ToList();

            return Ok(result);
        }

        /// <summary>
        /// Get One User Profile
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet("User/{key}")]
        [EnableQuery]
        public ActionResult GetOne(string key)
        {
            var result = userprofileService.GetAll().Where(x => x.Id == key);
            return Ok(SingleResult.Create(result));
        }

    }
}
