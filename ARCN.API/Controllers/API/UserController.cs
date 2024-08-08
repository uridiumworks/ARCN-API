
using ARCN.API.Atrributes;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.FluentValidations.ApplicationUser;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using NovaBank.Application.FluentValidations.ApplicationUser;

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


        [MustHavePermission(AppFeature.Users, AppAction.Create)]
        [HttpPost("CreateUser")]
        public async ValueTask<ActionResult> CreateUser([FromBody] NewUserDataModel adminUserProfileData)
        {

            var result = await userSettingService.CreateAdminUser(adminUserProfileData);

            return Ok(result);
        }

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
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<ResetPasswordModel>> ResetPassword(ResetPasswordModel resetPassword)
        {

            if (!ModelState.IsValid) return BadRequest();

            var response = await registerUserService.ResetPassword(resetPassword);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await registerUserService.ForgotPassword(forgotPassword);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpPost("ResetForgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<ResetPasswordModel>> ResetForgotPassword(ResetForgotPasswordModel resetPassword)
        {

            if (!ModelState.IsValid) return BadRequest();

            var response = await registerUserService.ResetForgotPassword(resetPassword);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }
    }
}
