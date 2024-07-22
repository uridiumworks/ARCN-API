
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Errors = ARCN.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using System.Globalization;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces;
using ARCN.Application.TokenProviders;
using ARCN.Application.Interfaces.Services;
using ARCN.Application;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class RegisterService : IRegisterService
    {
        private readonly ILogger<RegisterService> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly IValidator<ForgotPassword> forgotPasswordValidator;
        private readonly IValidator<ResetPasswordModel> resetPasswordValidator;
        private readonly IMediator mediator;
        private readonly IEmailService emailService;
        private readonly IValidator<NewUserDataModel> validatorNewUser;
        private readonly IPasswordValidator<ApplicationUser> passwordValidator;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserIdentityService userIdentityService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISecurityQuestionAnswerRepository securityQuestionAnswerRepository;
        private readonly ISecurityQuestionRepository securityQuestionRepository;
        private readonly IUserPolicy _userPolicy;
        private readonly ICloudinaryFileUploadService cloudinaryFileUploadService;

        public RegisterService(
            ILogger<RegisterService> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IUserService userService,
            IValidator<ForgotPassword> forgotPasswordValidator,
            IValidator<ResetPasswordModel> resetPasswordValidator,
            IMediator mediator,
            IEmailService emailService,
            IPasswordValidator<ApplicationUser> passwordValidator,
            IUserProfileRepository userProfileRepository,
            IUserIdentityService userIdentityService,
            IUnitOfWork unitOfWork,
            ISecurityQuestionAnswerRepository securityQuestionAnswerRepository,
            ISecurityQuestionRepository securityQuestionRepository,
            IUserPolicy userPolicy,
            ICloudinaryFileUploadService cloudinaryFileUploadService)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.userService = userService;
            this.forgotPasswordValidator = forgotPasswordValidator;
            this.resetPasswordValidator = resetPasswordValidator;
            this.mediator = mediator;
            this.emailService = emailService;
            this.validatorNewUser = validatorNewUser;
            this.passwordValidator = passwordValidator;
            this.userProfileRepository = userProfileRepository;
            this.userIdentityService = userIdentityService;
            this.unitOfWork = unitOfWork;
            this.securityQuestionAnswerRepository = securityQuestionAnswerRepository;
            this.securityQuestionRepository = securityQuestionRepository;
            _userPolicy = userPolicy;
            this.cloudinaryFileUploadService = cloudinaryFileUploadService;

        }

        public async ValueTask<ResponseModel<RegisteredUserResponseDataModel>> NewUserAsync(NewUserDataModel userData, CancellationToken cancellationToken)
        {

            var validationResult = await validatorNewUser.ValidateAsync(userData);
            if (validationResult.Errors.Count > 0)
                throw new Errors.ValidationException(validationResult);

            var response = new RegisteredUserResponseDataModel();

            var userProfile = await userProfileRepository.Get(x => x.Email == userData.Email).FirstOrDefaultAsync();
            
            if (userProfile != null)
            {
            
            }

            var appUser = userService.CreateNewUser(userData);

            var passwordValid = await passwordValidator.ValidateAsync(userManager, appUser, userData.Password);

            if (!passwordValid.Succeeded)
            {
                logger.LogError("New User registration failed {0}", userData.Email);

                var validationError = new List<FluentValidation.Results.ValidationFailure>();
                var errorResult = new ValidationResult();
                foreach (var error in passwordValid.Errors)
                {
                    validationError.Add(new FluentValidation.Results.ValidationFailure("Password", error.Description));
                }
                errorResult.Errors = validationError;
                throw new Errors.ValidationException(errorResult);
            }
            logger.LogInformation("New User registration successful {0}", userData.Email);
            //Generate refreshToken
            appUser.RefreshToken = tokenService.GenerateRefreshToken();
            appUser.RefreshTokenExpiryDate = DateTime.Now.AddHours(24);
         
            var result = await userService.CreateOrUpdateUserAsync(appUser, userData.Password);
            if (result.Item1.Succeeded)
            {
                appUser = result.Item2;
                var hashPassword = userManager.PasswordHasher.HashPassword(appUser, userData.Password);
                await _userPolicy.AddPasswordHistory(appUser.Id, hashPassword, appUser?.UserName);
                await userManager.UpdateAsync(appUser);
                //Generate accountNumber
                return new ResponseModel<RegisteredUserResponseDataModel> { Success = false, Message = "AccountOpeningError", Errors = new List<string> { "unable to connect to account opening api at this time" } };
            }
            else
            {
                var validationError = new List<FluentValidation.Results.ValidationFailure>();
                var errorResult = new ValidationResult();
                foreach (var error in result.Item1.Errors)
                {
                    validationError.Add(new FluentValidation.Results.ValidationFailure(error.Code, error.Description));
                }
                errorResult.Errors = validationError;
                throw new Errors.ValidationException(errorResult);
            }

        }


        public async ValueTask<ResponseModel<string>> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
           
            ///This wont work with accountNumber again
            var profile = userProfileRepository.Get(x => x.Email == forgotPassword.email).FirstOrDefault();
            if (profile == null)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid User, please contact the admin"
                };
            }

            var user = await userManager.FindByEmailAsync(profile.Email);
            if (user == null)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid User, please contact the admin",
                };
            }
            var questionIdAndAnswer = forgotPassword.SecurityQuestionAnswers.Select(x => new { x.QuestionId, x.Answer }).ToDictionary(t => t.QuestionId, t => t.Answer);
            var isValidSecurityQusetion = securityQuestionAnswerRepository.ValidateSecurityQuestionAndAnswers(questionIdAndAnswer, profile.Id, user);

            if (!isValidSecurityQusetion)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "One of the security questions answer is wrong!",
                };

            }
            var otp = await userManager.GenerateTwoFactorTokenAsync(user, AppTokenProvider.TotpProvider);

            await emailService.PasswordReset(user, otp);

            //var userResult = await userService.UserResponses(user);
            //string userName = profile.FirstName + " " + profile.LastName;
            //var token = await userManager.GeneratePasswordResetTokenAsync(user);
            //var encryptedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            //var userData = new ForgotPasswordMail
            //{
            //    Email = user.Email,
            //    Token = encryptedToken,
            //    Name = userName
            //};

            //await emailService.ForgotPasswordMail(userData);

            return new ResponseModel<string>
            {
                Success = true,
                Message = "Otp notification has been sent to you.",
                Data = await tokenService.CreateTokenAsync(user)
            };
        }

        public async ValueTask<ResponseModel<ApplicationUser>> ResetPasswordAsync(ResetPasswordModel resetPassword)
        {
            var profile = await userProfileRepository.FindByIdAsync(userIdentityService.UserProfileId);
            var user = await userManager.FindByEmailAsync(profile.Email);
            if (user == null)
            {
                return new ResponseModel<ApplicationUser>
                {
                    Success = false,
                    Message = "Invalid email address.",
                    Data = null
                };
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encryptedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // var token = resetPassword.Token;
            //byte[] decodeToken = WebEncoders.Base64UrlDecode(token);
            //var tokenstr = Encoding.UTF8.GetString(decodeToken);
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encryptedToken));

            var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPassword.ConfirmPassword);
            if (result.Succeeded)
            {
                // Update the security stamp to invalidate old sessions
                await userManager.UpdateSecurityStampAsync(user);

                return new ResponseModel<ApplicationUser>
                {
                    Success = true,
                    Message = "Password has been reset successfully.",
                    Data = null
                };
            }
            else
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return new ResponseModel<ApplicationUser>
                {
                    Success = false,
                    Message = string.Join(", ", errorMessages),
                    Data = null
                };
            }
        }
        public async ValueTask<ResponseModel<UserResponseDataModel>> LoginAsync(LoginDataModel login)
        {

            var user = new ApplicationUser();

            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = false,
                    Message = "Please fill all required fields!",
                    Data = null
                };

            }

            user = await userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                var profile = await userProfileRepository.Get(x => x.UserName.ToLower() == login.Email.ToLower()).FirstOrDefaultAsync();
                if (profile == null)
                {
                    return new ResponseModel<UserResponseDataModel>
                    {
                        Success = false,
                        Message = "User Credential is incorrect!",
                        Data = null
                    };

                }
                user = await userManager.FindByIdAsync(profile.Id);
            }

            if (!user.EmailConfirmed)
            {

                var result = await userManager.GenerateTwoFactorTokenAsync(user, AppTokenProvider.TotpProvider);
                await emailService.ConfirmEmailAddress(result, user);
                var response = await userService.UserResponses(user, string.Empty, string.Empty);

                return new ResponseModel<UserResponseDataModel>
                {
                    Success = false,
                    Message = "A confirmation email has been sent. Please check to verify this email. ",
                    Data = response
                };
            }

            var resultCheck = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);




            if (resultCheck.Succeeded)
            {

                var oldClm = (await userManager.GetClaimsAsync(user)).Where(x => x.Type == AppClaimType.SecurityStamp).FirstOrDefault(); //new Claim(AppClaimType.SecurityStamp, user?.SecurityStamp);
                if (oldClm != null)
                {

                    // Update the security stamp to invalidate old sessions
                    await userManager.UpdateSecurityStampAsync(user);

                    await userManager.ReplaceClaimAsync(user, oldClm, new Claim(AppClaimType.SecurityStamp, user?.SecurityStamp));
                }

                user.RefreshToken = tokenService.GenerateRefreshToken();
                user.RefreshTokenExpiryDate = DateTime.Now.AddHours(24);
                await userManager.UpdateAsync(user);

               
                var response = await userService.UserResponses(user, string.Empty, string.Empty);

                return new ResponseModel<UserResponseDataModel>
                {
                    Success = true,
                    Message = "Successfully login.",
                    Data = response
                };
            }
            return new ResponseModel<UserResponseDataModel>
            {
                Success = false,
                Message = "User credential is incorrect!",
            };
        }

        public async ValueTask<string> VerifyOtp(VerifyOTPDataModel verifyOtp)
        {
            var profile = await userProfileRepository.FindByIdAsync(userIdentityService.UserProfileId);
            var user = await userManager.FindByIdAsync(profile.Id);


            var result = await userManager.VerifyTwoFactorTokenAsync(user, AppTokenProvider.TotpProvider, verifyOtp.OTP);
            if (result)
            {
                user.EmailConfirmed = true;
                userProfileRepository.Update(profile);
                await unitOfWork.SaveChangesAsync();
                await userManager.UpdateAsync(user);

                return "success";
            }
            else
            {
                return "Verification Failed! Kindly insert a valid otp";
            }


        }

        public async ValueTask<string> ResendOTP()
        {
            var profile = await userProfileRepository.FindByIdAsync(userIdentityService.UserProfileId);
            var user = await userManager.FindByIdAsync(profile.Id);
            // var user = await userManager.FindByEmailAsync(resendOTP.Email);
            user = await userProfileRepository.FindAll().Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            var result = await userManager.GenerateTwoFactorTokenAsync(user, AppTokenProvider.TotpProvider);

            await emailService.ConfirmEmailAddress(result, user);
            return result;
        }
        public async ValueTask<bool> ValidateRefreshToken(string refreshToken, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryDate <= DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public async ValueTask<TokenDataModel> RefreshTokenAsync(string refreshToken, ApplicationUser user)
        {
            var result = await ValidateRefreshToken(refreshToken, user.Id);

            if (!result) throw new SecurityTokenException("Invalid refresh token");

            var newTokens = new TokenDataModel
            {
                Token = await tokenService.CreateTokenAsync(user),
                RefreshToken = tokenService.GenerateRefreshToken()
            };

            return newTokens;
        }

    }

}
