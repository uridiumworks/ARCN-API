

using System.Globalization;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ILogger<UserService> logger;
        private readonly ARCNDbContext context;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserIdentityService userIdentityService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<UserService> logger,
            ARCNDbContext context,
            IMapper mapper,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IUserIdentityService userIdentityService,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask AddUserClaimsAsync(ApplicationUser user, List<Claim> claims)
        {
            try
            {
                await userManager.AddClaimsAsync(user, claims);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }
        }

        public async ValueTask<(IdentityResult, ApplicationUser?)> CreateOrUpdateUserAsync(ApplicationUser applicationUser, string password)
        {
            var existingUser = await userManager.Users.FirstOrDefaultAsync(x => x.Email == applicationUser.Email);

            if (existingUser != null)
            {
                existingUser.UpdateUser(applicationUser);
                var updateResult = await userManager.UpdateAsync(existingUser);
                if (!updateResult.Succeeded)
                {
                    return (updateResult, null);
                }

                if (!string.IsNullOrEmpty(password))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(existingUser);
                    var passwordResult = await userManager.ResetPasswordAsync(existingUser, token, password);

                    if (!passwordResult.Succeeded)
                    {
                        return (passwordResult, null);
                    }
                }
                await unitOfWork.SaveChangesAsync();
                return (IdentityResult.Success, existingUser);
            }
            var result = await userManager.CreateAsync(applicationUser, password);
            return (result, applicationUser);
        }

        public async ValueTask<IdentityResult> CreateExistingUserAsync(ApplicationUser applicationUser)
        {
            var existingUser = await userManager.Users.FirstOrDefaultAsync(x => x.Email == applicationUser.Email);

            if (existingUser != null)
            {
                existingUser.PhoneNumber = applicationUser.PhoneNumber;
                existingUser.UserName = applicationUser.UserName;
                existingUser.Email = applicationUser.Email;
                existingUser.FirstName = applicationUser.FirstName;
                existingUser.LastName = applicationUser.LastName;
                existingUser.DateOfBirth = applicationUser.DateOfBirth;
                existingUser.PermanentAddress = applicationUser.PermanentAddress;
                var updateResult = await userManager.UpdateAsync(existingUser);

                if (!updateResult.Succeeded)
                {
                    return updateResult;
                }

                await unitOfWork.SaveChangesAsync();
                return IdentityResult.Success;
            }
            var result = await userManager.CreateAsync(applicationUser);
            return result;
        }


        public async ValueTask<UserResponseDataModel> UserResponses(ApplicationUser user, string acctNum, string accountType)
        {

            logger.LogInformation("generating access token for user {0}", user.Email);
            var res = new UserResponseDataModel
            {
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                MiddleName = user?.MiddleName,
                Email = user?.Email,
                PhoneNumber = user?.PhoneNumber,
                UserName = user?.UserName,
                ProfileImgUrl = user?.ProfileImageUrl,
                RefreshToken = user?.RefreshToken,
                Token = await tokenService.CreateTokenAsync(user)
            };

            return res;
        }

        public ApplicationUser CreateNewUser(NewUserDataModel userData)
        {
            return new ApplicationUser
            {
                PhoneNumber = userData.PhoneNumber,
                UserName = userData.UserName,
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                MiddleName = userData.MiddleName,
                DateOfBirth = userData.DateOfBirth,
                ProfileImageUrl = userData.ProfileImageUrl,
                StateId = userData.StateId,
                PermanentAddress = userData.PermanentAddress
            };
        }

        public bool ValidateExistingEmail(string email, string userId = "")
        {
            return string.IsNullOrWhiteSpace(userId) ? userManager.Users.Any(x => x.Email.ToLower() == email.ToLower()) : userManager.Users.Any(x => x.Email.ToLower() == email.ToLower() && x.Id != userId);
        }

    }
}
