
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Services;
using ARCN.Application.Interfaces;
using ARCN.Domain.Commons.Authorization;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Repositories;
using FluentValidation.Results;
using FluentValidation.AspNetCore;
using MR3.Application.DataModels.UserProfile;
using ARCN.API.Atrributes;

namespace ARCN.API.Controllers.API
{
    [Route("api/[controller]")]
    //[AllowAnonymous]
    public class UserController : ODataController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;
        private readonly IValidator<NewUserDataModel> registerDataModelValidator;
        private readonly IValidator<ForgotPassword> forgotPasswordValidator;
        private readonly IValidator<ResetPasswordModel> resetPasswordValidator;
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPasswordValidator<ApplicationUser> passwordValidator;
        private readonly IEmailSenderService emailService;
        private readonly ITokenService tokenService;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IValidator<AddAdmUserPasswordDataModel> validatorAddAdmUser;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, ILogger<UserController> logger, IUserService userService,
             IValidator<NewUserDataModel> registerDataModelValidator,
             IValidator<ForgotPassword> forgotPasswordValidator,
             IValidator<ResetPasswordModel> resetPasswordValidator, IMediator mediator, IUnitOfWork unitOfWork, 
             IPasswordValidator<ApplicationUser> passwordValidator,
             IEmailSenderService emailService,
             ITokenService tokenService,
             IUserProfileRepository userProfileRepository,
              IValidator<AddAdmUserPasswordDataModel> validatorAddAdmUser)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.userService = userService;
            this.registerDataModelValidator = registerDataModelValidator;
            this.forgotPasswordValidator = forgotPasswordValidator;
            this.resetPasswordValidator = resetPasswordValidator;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
            this.passwordValidator = passwordValidator;
            this.emailService = emailService;
            this.tokenService = tokenService;
            this.userProfileRepository = userProfileRepository;
            this.validatorAddAdmUser = validatorAddAdmUser;
        }

        [HttpPost("RegisterAdminUser")]
        [MustHavePermission(AppFeature.Users, AppAction.Create)]
      // [AllowAnonymous]
        public async ValueTask<ActionResult<ApplicationUser>> RegisterAdminUser([FromBody] NewUserDataModel register)
        {
            string requestModel = JsonConvert.SerializeObject(register);
            logger.LogInformation("RegisterAdminUser Request Model {0}", requestModel);

 
            var validatorResult = await registerDataModelValidator.ValidateAsync(register);
            if (!validatorResult.IsValid)
            {

                validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                //logger.LogInformation("{0}", ModelState);
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var adminUser = new ApplicationUser
            {
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                UserName = register.Email,
                NormalizedUserName = register.Email,
                IsAdmin = true,
                FirstName = register.FirstName,
                LastName = register.LastName,

            };

            if (await userManager.Users.AnyAsync(x => x.Email == register.Email))
            {
                ModelState.AddModelError("Email", "Email already exist!");
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var result = await userManager.CreateAsync(adminUser);
            if (result.Succeeded)
            {
                logger.LogInformation("User created successfully with Email {0}", adminUser.Email);
                // var otp = await otpService.GenerateTokenAsync(aspUser, "otp");
                await userManager.AddToRoleAsync(adminUser, AppRoles.Staff);
                await userService.AddUserClaimsAsync(adminUser);
                var userData = await userService.AdminUserResponses(adminUser);

                // TODO: Email service for confirmation link
                // var newUser = await userManager.FindByEmailAsync(userData.Email);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
                var newToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                await emailService.ConfirmEmailAddress(newToken, adminUser);

                //ToDo: Email service to password

                string responseModel = JsonConvert.SerializeObject(userData);
                logger.LogInformation("Register Response Model {0}", responseModel);

                return Ok(userData);
            }

            return Ok();
        }



        [HttpPost("Get-Token")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<TokenResponse>> GetToken([FromBody] TokenRequest request)
        {
            var token = tokenService.CreateTokenAsync(request);
            return Ok(token);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<UserResponseDataModel>> Login([FromBody] LoginDataModel login)
        {

            string requestModel = JsonConvert.SerializeObject(login);
            logger.LogInformation("Admin Login Request Model {0}", requestModel);

            var user = new ApplicationUser();

            if (!string.IsNullOrEmpty(login.Email))
            {
                user = await userManager.FindByEmailAsync(login.Email);
            }

            if (user == null)
            {
                ModelState.AddModelError("User", "User Credential is incorrect!");
                logger.LogInformation("{0}", ModelState);
                return ValidationProblem(instance: "101", modelStateDictionary: ModelState);
            }

            //user.AdminUserProfile = await (await mediator.Send(new GetAdminUserProfileQuery()))
            //    .Where(x => x.ApplicationUserId == user.Id).FirstOrDefaultAsync();

            if (!user.EmailConfirmed)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                await emailService.ConfirmEmailAddress(token, user);

                ModelState.AddModelError("Email", "A confirmation email has been sent. Please check to verify this email.");
                return ValidationProblem(instance: "103", modelStateDictionary: ModelState);
            }

            var resultCheck = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            user = await userProfileRepository.FindAll().FirstOrDefaultAsync(s => s.Id == user.Id);
            if (resultCheck.Succeeded)
            {
                logger.LogInformation("User with email {0} logged in successfully", user.Email);
                return await userService.AdminUserResponses(user);
            }

            ModelState.AddModelError("User", "User credential is incorrect!");
            logger.LogInformation("{0}", ModelState);
            return ValidationProblem(instance: "101", modelStateDictionary: ModelState);
        }


        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailDataModel confirmEmail)
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmEmail.Token));
            logger.LogInformation("Email: {0} ", confirmEmail.Email);
            logger.LogInformation("Token: {0} ", code);
            var user = await userManager.FindByEmailAsync(confirmEmail.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Email is incorrect!");
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }


            var result = await userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded) { return Ok(); }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return ValidationProblem(ModelState);
            }
        }



        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<ResetPasswordModel>> ResetPassword(ResetPasswordModel resetPassword)
        {
            //if (parameters == null || !parameters.TryGetValue("model", out object? modelOutput))
            //{
            //    ModelState.AddModelError("model", "model field is required");
            //    return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            //}

            //ResetPasswordModel resetPassword = (ResetPasswordModel)modelOutput;

            ValidationResult res = await resetPasswordValidator.ValidateAsync(resetPassword);
            if (!res.IsValid)
            {
                res.AddToModelState(this.ModelState);
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
            // var userId = User.GetSubjectId();
            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                ModelState.AddModelError("ResetPassword", "Invalid User!");
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var token = resetPassword.Token;
            byte[] decodeToken = WebEncoders.Base64UrlDecode(token);
            var tokenstr = Encoding.UTF8.GetString(decodeToken);

            var result = await userManager.ResetPasswordAsync(user, tokenstr, resetPassword.ConfirmPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }

                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            //if (parameters == null || !parameters.TryGetValue("model", out object? modelOutput))
            //{
            //    ModelState.AddModelError("model", "model field is required");
            //    return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            //}

            //ForgotPassword forgotPassword = (ForgotPassword)modelOutput;

            ValidationResult res = await forgotPasswordValidator.ValidateAsync(forgotPassword);
            if (!res.IsValid)
            {
                res.AddToModelState(this.ModelState);
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
            var user = await userManager.FindByEmailAsync(forgotPassword.email);
            var profile = await userProfileRepository.FindAll()
                .Where(x => x.Id == user.Id).FirstOrDefaultAsync();


            if (user == null)
            {
                ModelState.AddModelError("Email", "Email is not attached to account!");
                return ValidationProblem(instance: "104", modelStateDictionary: ModelState);

            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encryptedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var userData = new ForgotPasswordMail
            {
                Email = forgotPassword.email,
                Token = encryptedToken,
                Name = profile.FirstName
            };

            await emailService.ForgotPasswordMail(userData);
            return Ok();
        }

        [HttpPost("AddUserPassword")]
        [AllowAnonymous]
        public async ValueTask<ActionResult> AddUserPassword([FromBody] AddAdmUserPasswordDataModel dataModel)
        {
            var validatorResult = await validatorAddAdmUser.ValidateAsync(dataModel);

            if (!validatorResult.IsValid)
            {
                validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }



            var user = await userManager.FindByEmailAsync(dataModel.Email);

            if (user == null) ModelState.AddModelError("User", "User does not exist!");



            var passwordValid = await passwordValidator.ValidateAsync(userManager, user, dataModel.Password);

            if (!passwordValid.Succeeded)
            {
                foreach (var item in passwordValid.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }

                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);


            var result = await userManager.AddPasswordAsync(user, dataModel.Password);


            if (result.Succeeded)
            {
                return Ok(new { Success = "Password added successfully!" });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }

                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
        }

    }


}
