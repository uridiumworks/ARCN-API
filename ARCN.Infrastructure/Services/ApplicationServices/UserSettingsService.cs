using FluentValidation;
using FluentValidation.Results;
using ARCN.Application;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using Errors = ARCN.Application.Exceptions;
using ARCN.Application.DataModels.UserProfile;
using Microsoft.EntityFrameworkCore;
using ARCN.Application.DataModels.Identity;
using ARCN.Domain.Commons.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class UserSettingsService : IUserSettingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ARCNDbContext context;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IValidator<NewUserDataModel> validator;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IEmailSenderService emailSenderService;

        public UserSettingsService(IUnitOfWork unitOfWork, ARCNDbContext context, IUserProfileRepository userProfileRepository, 
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IMapper mapper, IValidator<NewUserDataModel> validator, IPasswordHasher<ApplicationUser> passwordHasher,
            ITokenService tokenService,IUserService userService,IUserIdentityService userIdentityService,IEmailSenderService emailSenderService)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.userProfileRepository = userProfileRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.tokenService = tokenService;
            this.userService = userService;
            this.userIdentityService = userIdentityService;
            this.emailSenderService = emailSenderService;
        }

        public async ValueTask<ResponseModel<UserResponseDataModel>> CreateAdminUser(NewUserDataModel adminUser)
        {
            var validatorResult = await validator.ValidateAsync(adminUser);

            if (!validatorResult.IsValid)
            {
                var err = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseModel<UserResponseDataModel>.ErrorMessage("error", err);
            }

            var user = new ApplicationUser
            {
                FirstName = adminUser.FirstName,
                LastName = adminUser.LastName,
                Email = adminUser.Email,
                PhoneNumber = adminUser.PhoneNumber,
                UserName = adminUser.Email,
                RefreshToken = tokenService.GenerateRefreshToken(),
                RefreshTokenExpiryDate = DateTime.Now.AddHours(24),
                IsAdmin = true,
            };

            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                //int passwordLength = 8; // Specify desired password length
                //string password = GenerateRandomPassword(passwordLength);
                //var PasswordResponse = await userManager.AddPasswordAsync(user, password);
                //if (!PasswordResponse.Succeeded)
                //{
                //    return new ResponseModel<UserResponseDataModel>
                //    {
                //        Success = true,
                //        Message = "Password Not successfully added",
                //        StatusCode = 200
                //    };
                //}
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.GivenName, user.FirstName??""),
                        new Claim(AppClaimType.ProfileId, user.Id.ToString()),
                        new Claim(ClaimTypes.Surname,  user.LastName),
                        new Claim(ClaimTypes.Email,  user.Email),
                        new Claim(AppClaimType.SecurityStamp, user.SecurityStamp)
                    };
                await userService.AddUserClaimsAsync(user, claims);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var newToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                await emailSenderService.ConfirmEmailAddress(newToken, user);

                var res = await userService.UserResponses(user);
                return ResponseModel<UserResponseDataModel>.SuccessMessage("success", data:res);

            }
            else
            {
                var validationError = new List<ResponseModel<UserResponseDataModel>>();
                var errorResult = new ValidationResult();
                foreach (var error in result.Errors)
                {
                    return new ResponseModel<UserResponseDataModel>
                    {
                        Success = false,
                        Message = error.Description,
                        StatusCode = 500
                    };
                }

                return default;


            }
            

        }

        

        public async ValueTask<ResponseModel<List<ApplicationUser>>> GetAllUsers()
        {
            var adminUsers = await userManager.Users.Where(x=>x.IsAdmin).ToListAsync();

            return ResponseModel<List<ApplicationUser>>.SuccessMessage("success", adminUsers);
        }

        public async ValueTask<ResponseModel<ApplicationUser>> GetProfile()
        {
            var adminUser = await userManager.Users.Where(x => x.IsAdmin && x.Id == userIdentityService.UserId).FirstOrDefaultAsync();

            return ResponseModel<ApplicationUser>.SuccessMessage("success", adminUser);
        }

        public async ValueTask<bool> RemoveAdminUser(string id)
        {
            var adminUsers = await userManager.FindByIdAsync(id);

            var res = await userManager.DeleteAsync(adminUsers);
            return res.Succeeded;
        }


        public async ValueTask<ResponseModel<ApplicationUser>> UpdateUser(NewUserDataModel userData)
        {
            var validatorResult = await validator.ValidateAsync(userData);

            if (!validatorResult.IsValid)
            {
                var err = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return ResponseModel<ApplicationUser>.ErrorMessage("error", err);
            }

            var adminUsers = await userManager.FindByEmailAsync(userData.Email);

            mapper.Map(userData, adminUsers);

            var res = await userManager.UpdateAsync(adminUsers);


            if (res.Succeeded) return ResponseModel<ApplicationUser>.SuccessMessage("success", adminUsers);

            return ResponseModel<ApplicationUser>.ErrorMessage("Error", res.Errors.Select(x => x.Description).ToList());

        }

        public async ValueTask<ResponseModel<List<RoleClaimResponseDataModel>>> GetAllRoleClaims()
        {
            var roles = await roleManager.Roles.ToListAsync();

            var rolesAndClaims = new List<RoleClaimResponseDataModel>();
            foreach (var role in roles)
            {

                var allPermissions = AppPermissions.AllPermissions;
                var roleclaimRes = new RoleClaimResponseDataModel
                {
                    Role = new()
                    {
                        Id = role.Id,
                        Name = role.Name
                    },

                    RoleClaims = new()

                };

                var currentRoleClaim = await GetAllClaimsForRoleAsync(role.Id);
                var allPermissionsName = allPermissions.Select(x => x.Name).ToList();
                var currentClaimValues = currentRoleClaim.Select(c => c.ClaimValue).ToList();

                var currentlyAssignedRoleClaimsNames = allPermissionsName.Intersect(currentClaimValues).ToList();

                foreach (var item in allPermissions)
                {
                    if (currentlyAssignedRoleClaimsNames.Any(c => c == item.Name))
                    {
                        roleclaimRes.RoleClaims.Add(new RoleClaimDataModel
                        {
                            RoleId = role.Id,
                            ClaimType = AppClaim.Permission,
                            Description = item.description,
                            ClaimValue = item.Name,
                            Group = item.group,
                            IsAssignedToRole = true
                        });
                    }
                    else
                    {
                        roleclaimRes.RoleClaims.Add(new RoleClaimDataModel
                        {
                            RoleId = role.Id,
                            ClaimType = AppClaim.Permission,
                            Description = item.description,
                            ClaimValue = item.Name,
                            Group = item.group,
                            IsAssignedToRole = false
                        });
                    }


                }

                rolesAndClaims.Add(roleclaimRes);
            }

            return ResponseModel<List<RoleClaimResponseDataModel>>.SuccessMessage("success", rolesAndClaims);

        }

        public async ValueTask<ResponseModel<RoleClaimResponseDataModel>> GetPermissionAsync(string roleId)
        {
            var roleInDb = await roleManager.FindByIdAsync(roleId);

            if (roleInDb is not null)
            {
                var allPermissions = AppPermissions.AllPermissions;
                var roleclaimRes = new RoleClaimResponseDataModel
                {
                    Role = new()
                    {
                        Id = roleInDb.Id,
                        Name = roleInDb.Name
                    },

                    RoleClaims = new()

                };

                var currentRoleClaim = await GetAllClaimsForRoleAsync(roleId);
                var allPermissionsName = allPermissions.Select(x => x.Name).ToList();
                var currentClaimValues = currentRoleClaim.Select(c => c.ClaimValue).ToList();

                var currentlyAssignedRoleClaimsNames = allPermissionsName.Intersect(currentClaimValues).ToList();

                foreach (var item in allPermissions)
                {
                    if (currentlyAssignedRoleClaimsNames.Any(c => c == item.Name))
                    {
                        roleclaimRes.RoleClaims.Add(new RoleClaimDataModel
                        {
                            RoleId = roleId,
                            ClaimType = AppClaim.Permission,
                            Description = item.description,
                            ClaimValue = item.Name,
                            Group = item.group,
                            IsAssignedToRole = true
                        });
                    }
                    else
                    {
                        roleclaimRes.RoleClaims.Add(new RoleClaimDataModel
                        {
                            RoleId = roleId,
                            ClaimType = AppClaim.Permission,
                            Description = item.description,
                            ClaimValue = item.Name,
                            Group = item.group,
                            IsAssignedToRole = false
                        });
                    }
                }

                return ResponseModel<RoleClaimResponseDataModel>.SuccessMessage("success", roleclaimRes);

            }

            return ResponseModel<RoleClaimResponseDataModel>.SuccessMessage("success", null);
        }

        private async ValueTask<List<RoleClaimDataModel>> GetAllClaimsForRoleAsync(string roleId)
        {
            var roleclaims = await context.RoleClaims.Where(x => x.RoleId == roleId).ToListAsync();

            if (roleclaims.Count > 0)
            {
                var mappRoleClaim = mapper.Map<List<RoleClaimDataModel>>(roleclaims);
                return mappRoleClaim;
            }

            return new List<RoleClaimDataModel>();
        }

        public async ValueTask<ResponseModel<List<ApplicationRoleClaim>>> GetAllClaims()
        {
            var claims = await context.RoleClaims.ToListAsync();

            return ResponseModel<List<ApplicationRoleClaim>>.SuccessMessage("success", claims);
        }

        public async ValueTask<ResponseModel<List<RoleClaimDataModel>>> GetAllUnAssignedClaims()
        {
            var allPermissions = AppPermissions.AllPermissions;
            var roleclaims = new List<RoleClaimDataModel>();
            foreach (var item in allPermissions)
            {
                var claim = new RoleClaimDataModel
                {
                    ClaimType = AppClaim.Permission,
                    Description = item.description,
                    ClaimValue = item.Name,
                    Group = item.group,
                    IsAssignedToRole = false
                };
                roleclaims.Add(claim);
            }

            return ResponseModel<List<RoleClaimDataModel>>.SuccessMessage("", roleclaims);
        }

        public async ValueTask AddPermissionToRole(List<RoleClaimDataModel> roleClaimDataModels, string roleId)
        {
            foreach (var claim in roleClaimDataModels)
            {
                if (claim.IsAssignedToRole)
                {
                    var appRoleClaim = new ApplicationRoleClaim
                    {
                        RoleId = roleId,
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        Description = claim.Description,
                        Group = claim.Group
                    };
                    await context.RoleClaims.AddAsync(appRoleClaim);
                }
            }
            context.SaveChanges();
        }

        public async ValueTask<bool> UpdatePermissionAsync(UpdateRolePermissionRequestDataModel datamodel)
        {
            var role = await roleManager.FindByIdAsync(datamodel.RoleId);

            if (role != null)
            {

                var permissionToBeAssigned = datamodel.RoleClaims.Where(x => x.IsAssignedToRole).ToList();

                var currentlyAssignedClaims = await roleManager.GetClaimsAsync(role);

                foreach (var claim in currentlyAssignedClaims)
                {
                    await roleManager.RemoveClaimAsync(role, claim);
                }

                foreach (var claim in permissionToBeAssigned)
                {
                    var mapRoleClaim = mapper.Map<ApplicationRoleClaim>(claim);
                    await context.RoleClaims.AddAsync(mapRoleClaim);


                }
                context.SaveChanges();
                return true;
            }

            return false;

        }
        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+[]{}|;:,.<>?";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                password.Append(validChars[index]);
            }

            return password.ToString();
        }
    
}
}
