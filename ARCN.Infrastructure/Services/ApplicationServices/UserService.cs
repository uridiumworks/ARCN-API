

using System.Globalization;
using ARCN.Application.DataModels.Identity;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<UserService> logger;
        private readonly ITokenService tokenService;
        private readonly ARCNDbContext context;
        private readonly IMapper mapper;

        public UserService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ILogger<UserService> logger, ITokenService tokenService, ARCNDbContext context, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.tokenService = tokenService;
            this.context = context;
            this.mapper = mapper;
        }
        public async ValueTask AddUserClaimsAsync(ApplicationUser user)
        {
            try
            {
                if (user!=null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim("lastName",  user.LastName),
                        new Claim("ComfrimPhoneNumber",user.PhoneNumberConfirmed.ToString()),

                    };
                    await userManager.AddClaimsAsync(user, claims);

                }
                else
                {
                    #region Roles and Permission addUp
                    var userClaim = await userManager.GetClaimsAsync(user);
                    var roles = await userManager.GetRolesAsync(user);

                    var roleClaim = new List<Claim>();
                    var permissionClaim = new List<Claim>();

                    foreach (var role in roles)
                    {
                        roleClaim.Add(new Claim(ClaimTypes.Role, role));
                        var currentRole = await roleManager.FindByNameAsync(role);
                        var allPermissionForCurrentRole = await roleManager.GetClaimsAsync(currentRole);
                        permissionClaim.AddRange(allPermissionForCurrentRole);

                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim("email", user.Email),
                    }
                    .Union(userClaim)
                    .Union(roleClaim)
                    .Union(permissionClaim);
                    #endregion


                    await userManager.AddClaimsAsync(user, claims);

                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }
        }

        public async ValueTask<UserResponseDataModel> AdminUserResponses(ApplicationUser user)
        {
            var registeredModel = new UserResponseDataModel
            {
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user)

            };

            return registeredModel;
        }
       

        public async ValueTask<string> GenerateNewToken(ApplicationUser user)
        {
            return await tokenService.CreateTokenAsync(user);
        }

        public async ValueTask<List<RoleClaimResponseDataModel>> GetAllRoleClaims()
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
                        Name = role.Name,
                        //Description = role.Description
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

            return rolesAndClaims;

        }

        public async ValueTask<RoleClaimResponseDataModel> GetPermissionAsync(string roleId)
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
                        Name = roleInDb.Name,
                        //Description = roleInDb.Description
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

                return roleclaimRes;

            }

            return null;
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

        public async ValueTask<List<ApplicationRoleClaim>> GetAllClaims()
        {
            var claims = await context.RoleClaims.ToListAsync();

            return claims;
        }

        public async ValueTask<List<RoleClaimDataModel>> GetAllUnAssignedClaims()
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

            return roleclaims;
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

    }
}
