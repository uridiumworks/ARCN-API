using ARCN.Application;
using ARCN.Application.DataModels.ApplicationDataModels;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Services;
using ARCN.Application.TokenProviders;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ARCN.API.Controllers.API
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IRegisterService registerService;
        private readonly IValidator<ForgotPassword> forgotPasswordValidator;
        private readonly IValidator<NewUserDataModel> validatorNewUser;
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailService emailService;
        private readonly IValidator<VerifyOTPDataModel> validatorVerifyOtp;
        private readonly IValidator<ResendOTPDataModel> validatorResendOtp;
        private readonly IValidator<RefreshTokenDataModel> validatorRefreshToken;
        private readonly ITokenService tokenService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="forgotPasswordValidator"></param>
        /// <param name="validatorNewUser"></param>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        /// <param name="userManager"></param>
        /// <param name="emailService"></param>
        /// <param name="validatorVerifyOtp"></param>
        /// <param name="validatorResendOtp"></param>
        /// <param name="validatorRefreshToken"></param>
        /// <param name="tokenService"></param>
        public UserController(IRegisterService registerService, IValidator<ForgotPassword> forgotPasswordValidator,IValidator<NewUserDataModel> validatorNewUser,
             ILogger<UserController> logger,
             IUserService userService,
             UserManager<ApplicationUser> userManager,
             IEmailService emailService, IValidator<VerifyOTPDataModel> validatorVerifyOtp,
             IValidator<ResendOTPDataModel> validatorResendOtp,
             IValidator<RefreshTokenDataModel> validatorRefreshToken,
             ITokenService tokenService)
        {
            this.registerService = registerService;
            this.forgotPasswordValidator = forgotPasswordValidator;
            this.validatorNewUser = validatorNewUser;
            this.logger = logger;
            this.userService = userService;
            this.userManager = userManager;
            this.emailService = emailService;
            this.validatorVerifyOtp = validatorVerifyOtp;
            this.validatorResendOtp = validatorResendOtp;
            this.validatorRefreshToken = validatorRefreshToken;
            this.tokenService = tokenService;
        }


        /// <summary>
        /// Register New User
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Registration")]
        [Produces("application/json", Type = typeof(UserResponseDataModel))]
        public async ValueTask<ActionResult<string>> CreateNewUser([FromBody] NewUserDataModel userData, CancellationToken cancellationToken)
        {
            var newUserResponse = await registerService.NewUserAsync(userData, cancellationToken);
            if (!newUserResponse.Success)
                return BadRequest(newUserResponse);

            var newUserRes = newUserResponse.Data;
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.MobilePhone, newUserRes?.ApplicationUser?.PhoneNumber),
                        new Claim(ClaimTypes.NameIdentifier, newUserRes.ApplicationUser?.Id),
                        new Claim(ClaimTypes.GivenName, newUserRes.ApplicationUser?.FirstName??""),
                        new Claim(AppClaimType.ProfileId, newUserRes?.ApplicationUser.Id.ToString()),
                        new Claim(ClaimTypes.Surname,  newUserRes.ApplicationUser.LastName),
                        new Claim(ClaimTypes.Email,  newUserRes.ApplicationUser?.Email),
                        new Claim(AppClaimType.SecurityStamp, newUserRes?.ApplicationUser?.SecurityStamp)
                    };
            await userService.AddUserClaimsAsync(newUserRes?.ApplicationUser, claims);

            //generate OTP
            var otp = await userManager.GenerateTwoFactorTokenAsync(newUserRes?.ApplicationUser, AppTokenProvider.TotpProvider);
            logger.LogInformation("OTP {0} for user {1}", otp, newUserRes?.ApplicationUser?.Email);


            //send OTP
            await emailService.ConfirmEmailAddress(otp, newUserRes?.ApplicationUser);

            var userResult = await userService.UserResponses(newUserRes?.ApplicationUser, newUserRes?.AccountNumber, newUserRes?.AccountType);

            return Ok(userResult);

        }


        /// <summary>
        /// Register Existing User
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>

       
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [Produces("application/json", Type = typeof(ResponseModel<UserResponseDataModel>))]

        public async Task<ActionResult> Login(LoginDataModel login)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await registerService.LoginAsync(login);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

      
        /// <summary>
        /// Get New Refresh Token
        /// </summary>
        /// <param name="refreshTokenDataModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GetNewRefreshToken")]
        [Produces("application/json", Type = typeof(TokenDataModel))]
        public async ValueTask<ActionResult<TokenDataModel>> GetNewRefreshToken([FromBody] RefreshTokenDataModel refreshTokenDataModel)
        {
            var validatorResult = await validatorRefreshToken.ValidateAsync(refreshTokenDataModel);

            if (!validatorResult.IsValid)
            {
                validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var result = await registerService.ValidateRefreshToken(refreshTokenDataModel.RefreshToken, refreshTokenDataModel.ApplicationUserId);

            if (result)
            {
                var user = await userManager.FindByIdAsync(refreshTokenDataModel.ApplicationUserId);
                var token = await tokenService.CreateTokenAsync(user);
                return Ok(new TokenDataModel { RefreshToken = user.RefreshToken, Token = token });
            }

            return BadRequest("Invalid refresh token");
        }


        /// <summary>
        /// Verify OTP
        /// </summary>
        /// <param name="verifyOtp"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        //[HttpPost("VerifyOTP")]
        //public async ValueTask<ActionResult<bool>> VerifyOTP([FromBody] VerifyOTPDataModel verifyOtp)
        //{
        //    var validatorResult = await validatorVerifyOtp.ValidateAsync(verifyOtp);

        //    if (!validatorResult.IsValid)
        //    {
        //        validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
        //        return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
        //    }
        //    var res = await registerService.VerifyOtp(verifyOtp);
        //    if (res == "success") return Ok();


        //    return BadRequest(new { error = res });
        //}


        /// <summary>
        /// Resend OTP
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        //[HttpPost("ResendOTP")]
        //public async ValueTask<ActionResult> ResendOTP()
        //{
        //    var otp = await registerService.ResendOTP();
        //    return Ok();
        //}

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>

        //[AllowAnonymous]
        //[HttpPost("ForgotPassword")]
        //[Produces("application/json", Type = typeof(ResponseModel<string>))]

        //public async ValueTask<ActionResult> ForgotPassword(ForgotPassword forgotPassword)
        //{

        //    if (!ModelState.IsValid) return BadRequest();

        //    var response = await registerService.ForgotPasswordAsync(forgotPassword);
        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return BadRequest(response);
        //    }

        //}

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>

        //[AllowAnonymous]
        //[HttpPost("ResetPassword")]
        //[Produces("application/json", Type = typeof(ResponseModel<ApplicationUser>))]
        //public async ValueTask<ActionResult> ResetPassword(ResetPasswordModel resetPassword)
        //{
        //    if (!ModelState.IsValid) return BadRequest();

        //    var response = await registerService.ResetPasswordAsync(resetPassword);

        //    return Ok(response);
        //}



        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{

        //    return Ok(new { message = "Logout successful" });
        //}


      
    }
}
