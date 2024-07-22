
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ARCN.API.Controllers.API
{
    /// <summary>
    /// User Profile Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserprofileService userprofileService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userprofileService"></param>
        public UserProfileController(IUserprofileService userprofileService)
        {
            this.userprofileService = userprofileService;
        }


        /// <summary>
        ///  Update User Profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", Type = typeof(ResponseModel<ApplicationUser>))]
        public async ValueTask<ActionResult> Put(int id, [FromBody] ProfileUpdateDataModel profile)
        {
            var result = await userprofileService.UpdateUserProfile(id, profile);

            return Ok(result);
        }

      
    }
}
