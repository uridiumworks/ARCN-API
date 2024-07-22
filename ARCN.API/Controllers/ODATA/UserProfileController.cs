using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using Microsoft.AspNetCore.Components;
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
    public class UserProfileController : ODataController
    {
        private readonly IUserprofileService userprofileService;
        private readonly IUserProfileRepository userProfileRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userprofileService"></param>
        /// <param name="userProfileRepository"></param>
        public UserProfileController(IUserprofileService userprofileService, IUserProfileRepository userProfileRepository)
        {
            this.userprofileService = userprofileService;
            this.userProfileRepository = userProfileRepository;
        }



        /// <summary>
        /// Get All User Profile
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("UserProfile")]
        // [EnableQuery]
        public ActionResult<List<ApplicationUser>> GetAll()
        {
            var result = userProfileRepository.FindAll().ToList();

            return Ok(result);
        }

        //[HttpGet("UserProfile")]
        //[EnableQuery]
        //public async ValueTask<ActionResult> GetAll()
        //{ 
        //    var result =( await userprofileService.GetAll()).ToList();

        //    return Ok(result);
        //}

        /// <summary>
        /// Get One User Profile
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet("UserProfile/{key}")]
        [EnableQuery]
        public ActionResult GetOne(string key)
        {
            var result = userprofileService.GetAll().Where(x => x.Id == key);
            return Ok(SingleResult.Create(result));
        }

        /// <summary>
        /// Get Profile
        /// </summary>
        /// <returns></returns>
        //[HttpGet("GetProfile")]

        //[Produces("application/json", Type = typeof(ResponseModel<ApplicationUser>))]

        //public async ValueTask<ActionResult> GetProfile()
        //{
        //    var result = userprofileService.GetProfile();

        //    return Ok(result);
        //}


        //[HttpGet("GetUserProfile")]

        //[Produces("application/json", Type = typeof(ResponseModel<ApplicationUser>))]

        //public async ValueTask<ActionResult> GetUserProfile()
        //{
        //    var result = await userprofileService.GetUserProfile();

        //    return Ok(result);
        //}
    }
}
