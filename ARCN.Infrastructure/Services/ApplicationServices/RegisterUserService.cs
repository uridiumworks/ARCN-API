
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly ILogger<RegisterUserService> logger;
        private readonly IValidator<LoginDataModel> validator;
        private readonly ITokenService tokenService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserProfileRepository userProfileRepository;

        public RegisterUserService(ILogger<RegisterUserService> logger, IValidator<LoginDataModel> validator, 
            ITokenService tokenService, UserManager<ApplicationUser> userManager, 
            IUserProfileRepository userProfileRepository)
        {
            this.logger = logger;
            this.validator = validator;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.userProfileRepository = userProfileRepository;
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
