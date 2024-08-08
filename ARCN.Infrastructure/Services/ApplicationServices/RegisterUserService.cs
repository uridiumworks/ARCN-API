
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
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

        public RegisterUserService(ILogger<RegisterUserService> logger, IValidator<LoginDataModel> validator, 
            ITokenService tokenService, UserManager<ApplicationUser> userManager, 
            IUserProfileRepository userProfileRepository, IValidator<ResetPasswordModel> validatorResetPassword, 
            IUserIdentityService userIdentityService, IValidator<ForgotPassword> forgotPasswordValidator,
            IEmailSenderService emailSenderService)
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
                    Message = "Invalid user.",
                };
            }
            PasswordVerificationResult result = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, resetPassword.CurrentPassword);

            if (result != PasswordVerificationResult.Success)
            {
                return new ResponseModel<UserResponseDataModel>
                {
                    Success = false,
                    Message = "Incorrect Password",
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
                    Message = "Invalid user.",
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
                    Message = "Invalid user.",
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
    }
}
