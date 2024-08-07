
using ARCN.API.Atrributes;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARCN.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserSettingService userSettingService;
        private readonly IRegisterUserService registerUserService;

        public UserController(IUserService userService, IUserSettingService userSettingService
            ,IRegisterUserService registerUserService)
        {
            this.userService = userService;
            this.userSettingService = userSettingService;
            this.registerUserService = registerUserService;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="adminUserProfileData"></param>
        /// <returns></returns>
        [MustHavePermission(AppFeature.Users, AppAction.Create)]
        [HttpPost("CreateUser")]
        public async ValueTask<ActionResult> CreateUser([FromBody] NewUserDataModel adminUserProfileData)
        {
            var result = await userSettingService.CreateAdminUser(adminUserProfileData);

            return Ok(result);
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async ValueTask<ActionResult> LoginUser([FromBody] LoginDataModel dataModel)
        {
            var result = await registerUserService.Login(dataModel);

            return Ok(result);
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


        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        public async ValueTask<ActionResult> UpdateUser([FromBody] NewUserDataModel updateUser)
        {
            var res = await userSettingService.UpdateUser(updateUser);
            return Ok(res);
        }


        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser/{id}")]
        public async ValueTask<ActionResult> DeleteUser([FromRoute] string id)
        {
            var res = await userSettingService.RemoveAdminUser(id);
            return Ok(res);
        }
    }
}
