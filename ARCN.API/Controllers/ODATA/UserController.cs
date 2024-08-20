using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
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
    [AllowAnonymous]
    public class UserController : ODataController
    {
        private readonly IUserprofileService userprofileService;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserSettingService userSettingService;

        public UserController(IUserprofileService userprofileService, IUserProfileRepository userProfileRepository,
            IUserSettingService userSettingService)
        {
            this.userprofileService = userprofileService;
            this.userProfileRepository = userProfileRepository;
            this.userSettingService = userSettingService;
        }


        [HttpGet("GetAllUsers")]
        public async ValueTask<ActionResult> GetAllUsers()
        {
            var result = await userSettingService.GetAllUsers();
            return Ok(result);
        }

        /// <summary>
        /// Get Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProfile")]
        public async ValueTask<ActionResult> GetProfile()
        {
            var result = await userSettingService.GetProfile();
            return Ok(result);
        }

    }
}
