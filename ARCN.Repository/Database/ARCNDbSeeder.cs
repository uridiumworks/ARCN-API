
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using Microsoft.Extensions.Logging;

namespace ARCN.Repository.Database
{
    public class ARCNDbSeeder
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ARCNDbContext dbContext;
        private readonly ILogger<ARCNDbSeeder> logger;
        private readonly ITokenService tokenService;

        public ARCNDbSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ARCNDbContext dbContext, ILogger<ARCNDbSeeder> logger, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
            this.logger = logger;
            this.tokenService = tokenService;
        }

        public async Task SeedDatabaseAsync()
        {
            //Check Pending And Apply
            await CheckAndApplyPendingMigration();
            //Seed roles
            await SeedRolesAsync();
            //Seed AdminUser
            await SeedAdminUserAsync();
        }

        private async Task CheckAndApplyPendingMigration()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();

                logger.LogInformation("Database Migration Run Successfully");
            }

        }

        private async Task SeedRolesAsync()
        {
            foreach (var roleName in AppRoles.DefaultRoles)
            {
                if (await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName)
                    is not IdentityRole role)
                {
                    role = new IdentityRole
                    {
                        Name = roleName,
                       // Description = $"{roleName} Role"
                    };

                    await roleManager.CreateAsync(role);
                    logger.LogInformation("Role Added Successfully");
                }

                //Assign permission
                if (roleName == AppRoles.SuperAdmin)
                {
                    await AddPermissionToRoleAsync(role, AppPermissions.SuperAdminPermission);

                }
               
                else if (roleName == AppRoles.Staff)
                {
                    await AddPermissionToRoleAsync(role, AppPermissions.StaffPermission);
                }
               
                else if (roleName == AppRoles.Tech)
                {
                    await AddPermissionToRoleAsync(role, AppPermissions.TechPermission);
                }
               
                else if (roleName == AppRoles.Sales)
                {
                    await AddPermissionToRoleAsync(role, AppPermissions.SalesPermission);
                }
                else if (roleName == AppRoles.DataEntry)
                {
                    await AddPermissionToRoleAsync(role, AppPermissions.DataEntryPermission);
                }
            }
        }

        private async Task AddPermissionToRoleAsync(IdentityRole role, IReadOnlyList<AppPermission> permissions)
        {
            var currentClaims = await roleManager.GetClaimsAsync(role);

            foreach (var permission in permissions)
            {
                if (!currentClaims.Any(claim => claim.Type == AppClaim.Permission && claim.Value == permission.Name))
                {
                    await dbContext.RoleClaims.AddAsync(new ApplicationRoleClaim
                    {
                        RoleId = role.Id,
                        ClaimType = AppClaim.Permission,
                        ClaimValue = permission.Name,
                        Description = permission.description,
                        Group = permission.group,
                    });

                    logger.LogInformation($"Successfully added Permissions to Role - {role.Name}");

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task SeedAdminUserAsync()
        {
            string adminUserName = AppCredentials.Email[..AppCredentials.Email.IndexOf("@")].ToLowerInvariant();
            var adminUser = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = AppCredentials.Email,
                PhoneNumber = "08080000000",
                UserName = adminUserName,
                RefreshToken = tokenService.GenerateRefreshToken(),
                RefreshTokenExpiryDate = DateTime.Now.AddHours(24),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsAdmin = true,
            };

            if (!await userManager.Users.AnyAsync(u => u.Email == AppCredentials.Email))
            {
                var password = new PasswordHasher<ApplicationUser>();
                adminUser.PasswordHash = password.HashPassword(adminUser, AppCredentials.Password);

                await userManager.CreateAsync(adminUser);

                logger.LogInformation("SuperAdmin Created Successfully");

            }

            //Assign role to user
            if (!await userManager.IsInRoleAsync(adminUser, AppRoles.DataEntry) ||
                !await userManager.IsInRoleAsync(adminUser, AppRoles.SuperAdmin) ||
                !await userManager.IsInRoleAsync(adminUser, AppRoles.Staff))
            {
                // await userManager.AddToRolesAsync(adminUser, AppRoles.DefaultRoles);
                await userManager.AddToRoleAsync(adminUser, AppRoles.SuperAdmin);
                await userManager.AddToRoleAsync(adminUser, AppRoles.Staff);
                logger.LogInformation("SuperAdmin Added to Default Roles Successfully");
            }
        }
    }
}
