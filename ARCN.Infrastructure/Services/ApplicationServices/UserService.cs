

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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<UserService> logger;
        private readonly ARCNDbContext context;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserService> logger,
            ARCNDbContext context,
            IMapper mapper,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
            this.passwordHasher = passwordHasher;
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


        public async ValueTask<UserResponseDataModel> UserResponses(ApplicationUser user)
        {

            logger.LogInformation("generating access token for user {0}", user.Email);
            var res = new UserResponseDataModel
            {
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                Email = user?.Email,
                PhoneNumber = user?.PhoneNumber,
                UserName = user?.UserName,
                RefreshToken = user?.RefreshToken,
                Token = await tokenService.CreateTokenAsync(user)
            };

            return res;
        }

        public ApplicationUser CreateNewUser(NewUserDataModel userData, bool IsNewUser)
        {
            return new ApplicationUser
            {
                PhoneNumber = userData.PhoneNumber,
                UserName = userData.Email,
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName
            };
        }

    }
}
