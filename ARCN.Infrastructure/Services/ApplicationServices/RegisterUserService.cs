
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.FluentValidations.ApplicationUser;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MR3.Application.DataModels.UserProfile;
using Newtonsoft.Json;
using NovaBank.Application.FluentValidations.ApplicationUser;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly ILogger<RegisterUserService> logger;
        private readonly IValidator<LoginDataModel> validator;
        private readonly ITokenService tokenService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IValidator<ResetPasswordModel> validatorResetPassword;
        private readonly IUserIdentityService userIdentityService;
        private readonly IValidator<ForgotPassword> forgotPasswordValidator;
        private readonly IEmailSenderService emailSenderService;
        private readonly IPasswordValidator<ApplicationUser> passwordValidator;

        public RegisterUserService(ILogger<RegisterUserService> logger, IValidator<LoginDataModel> validator, 
            ITokenService tokenService, UserManager<ApplicationUser> userManager, 
            IUserProfileRepository userProfileRepository, IValidator<ResetPasswordModel> validatorResetPassword, 
            IUserIdentityService userIdentityService, IValidator<ForgotPassword> forgotPasswordValidator,
            IEmailSenderService emailSenderService, IPasswordValidator<ApplicationUser> passwordValidator)
        {
            this.logger = logger;
            this.validator = validator;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.userProfileRepository = userProfileRepository;
            this.validatorResetPassword = validatorResetPassword;
            this.userIdentityService = userIdentityService;
            this.forgotPasswordValidator = forgotPasswordValidator;
            this.emailSenderService = emailSenderService;
            this.passwordValidator = passwordValidator;
        }
        public async ValueTask<ResponseModel<UserResponseDataModel>> Login(LoginDataModel loginDataModel)
        {
            var validatorResult = await validator.ValidateAsync(loginDataModel);

            if (!validatorResult.IsValid)
            {
                var err = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseModel<UserResponseDataModel>.ErrorMessage("error", err);
            }

            var user = await userManager.FindByEmailAsync(loginDataModel.Email);
            if (user == null) throw new ValidationException($"User with email: {loginDataModel.Email} is not profile yet.");

            user = await userProfileRepository.FindAll().Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            if (!(user.Email == AppCredentials.Email))
            {
                    var userResponse = await CreateResponse(user);
                    return ResponseModel<UserResponseDataModel>.SuccessMessage("success", data: userResponse);
            }
            else
            {
                var userResponse = await CreateResponse(user);
                return ResponseModel<UserResponseDataModel>.SuccessMessage("success", data: userResponse);
            }

        }

        public async ValueTask<ResponseModel<UserResponseDataModel>> ResetPassword(ResetPasswordModel resetPassword)
        {
            var userId = userIdentityService.UserId;

            string requestModel = JsonConvert.SerializeObject(resetPassword);
            logger.LogInformation("Reset Password Request Model {0}", requestModel);

            var validatorResult = await validatorResetPassword.ValidateAsync(resetPassword);

            if (!validatorResult.IsValid)
            {
                var err = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseModel<UserResponseDataModel>.ErrorMessage("error", err);
            }
            
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode=404
                };
            }
            PasswordVerificationResult result = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, resetPassword.CurrentPassword);

            if (result != PasswordVerificationResult.Success)
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = false,
                    Message = "Incorrect Password",
                    StatusCode = 400
                };
            }
            var token = resetPassword.Token;
            byte[] decodeToken = WebEncoders.Base64UrlDecode(token);
            var tokenstr = Encoding.UTF8.GetString(decodeToken);

            var resp = await userManager.ResetPasswordAsync(user, tokenstr, resetPassword.ConfirmPassword);
            if (resp.Succeeded)
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = true,
                    Message = "Password reset successful",
                    StatusCode=200
                }; 
            }
            else
            {
                return ResponseModel<UserResponseDataModel>.ErrorMessage("Unable to reset password");
            }
        }
        public async ValueTask<ResponseModel<UserResponseDataModel>> ForgotPassword(ForgotPassword forgotPassword)
        {
           
            string requestModel = JsonConvert.SerializeObject(forgotPassword);
            logger.LogInformation("Forgot Request Model {0}", requestModel);


            var validatorResult  = await forgotPasswordValidator.ValidateAsync(forgotPassword);
            if (!validatorResult.IsValid)
            {
                var err = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseModel<UserResponseDataModel>.ErrorMessage("error", err);
            }

            var user = await userManager.FindByEmailAsync(forgotPassword.email);
    

            if (user == null)
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode=404
                };

            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encryptedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var userData = new ForgotPasswordMail
            {
                Email = forgotPassword.email,
                Token = encryptedToken,
                Name = user.FirstName+" "+user.LastName,
            };

            await emailSenderService.ForgotPasswordMail(userData);
            return new ResponseModel<UserResponseDataModel>
            {
                Success = true,
                Message = "Successfully",
                StatusCode= 200
            };
        }
        public async ValueTask<ResponseModel<UserResponseDataModel>> ResetForgotPassword(ResetForgotPasswordModel resetPassword)
        {
            
            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode=404
                };
            }
           
            var token = resetPassword.Token;
            byte[] decodeToken = WebEncoders.Base64UrlDecode(token);
            var tokenstr = Encoding.UTF8.GetString(decodeToken);

            var resp = await userManager.ResetPasswordAsync(user, tokenstr, resetPassword.ConfirmPassword);
            if (resp.Succeeded)
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = true,
                    Message = "Password reset successful",
                    StatusCode = 200
                };
            }
            else
            {
                return ResponseModel<UserResponseDataModel>.ErrorMessage("Unable to reset password");
            }
        }
        public async ValueTask<UserResponseDataModel> CreateResponse(ApplicationUser user)
        {
            var response = new UserResponseDataModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RefreshToken = user.RefreshToken,
                UserName = user.UserName,
                Token = await tokenService.CreateTokenAsync(user)

            };

            return response;
        }
        public async ValueTask<ResponseModel<ApplicationUser>> ConfirmEmail(ConfirmEmailDataModel confirmEmail)
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmEmail.Token));
            logger.LogInformation("Email: {0} ", confirmEmail.Email);
            logger.LogInformation("Token: {0} ", code);
            var user = await userManager.FindByEmailAsync(confirmEmail.Email);

            if (user == null)
            {
                return new ResponseModel<ApplicationUser>
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode = 404
                };
            }
            var result = await userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded) {

                return new ResponseModel<ApplicationUser>
                {
                    Success = true,
                    Message = "Password reset successful",
                    StatusCode=200
                };
            }
            else
            {
                var errors = new List<string>();
                foreach (var item in result.Errors)
                {
                   errors.Add(item.Description);
                }
                return new ResponseModel<ApplicationUser>
                {
                    Success = false,
                    Message = "Fail to confirm email",
                    StatusCode=400
                };
            }
        }

        public async ValueTask<ResponseModel<ApplicationUser>> AddUserPassword(AddAdmUserPasswordDataModel dataModel)
        {


            var user = await userManager.FindByEmailAsync(dataModel.Email);

            if (user == null)
            {
                return new ResponseModel<ApplicationUser>
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode=404
                };
            }

            var passwordValid = await passwordValidator.ValidateAsync(userManager, user, dataModel.Password);

            if (!passwordValid.Succeeded)
            {

                var errors = new List<string>();
                foreach (var item in passwordValid.Errors)
                {
                    errors.Add(item.Description);
                }
                return new ResponseModel<ApplicationUser>
                {
                    Success = false,
                    Message = "Fail to confirm email",
                    StatusCode=400
                };
            }

            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);


            var result = await userManager.AddPasswordAsync(user, dataModel.Password);


            if (result.Succeeded)
            {
                return new ResponseModel<ApplicationUser>
                {
                    Success = true,
                    Message = "Password added successfully!",
                    StatusCode= 200
                };
            }
            else
            {

                var errors = new List<string>();
                foreach (var item in result.Errors)
                {
                    errors.Add(item.Description);
                }
                return new ResponseModel<ApplicationUser>
                {
                    Success = false,
                    Message = "Fail to confirm email",
                    StatusCode = 400
                };
            }
        }

    }
}
