using ARCN.Application;
using ARCN.Application.Interfaces.Services;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<TokenService> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public TokenService(IConfiguration configuration,
            ILogger<TokenService> logger,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async ValueTask<string> CreateTokenAsync(ApplicationUser user)
        {
            try
            {
                IEnumerable<Claim> claims = await userManager.GetClaimsAsync(user);
                logger.LogInformation("User {0} has {1} claims", user.Email, claims.ToList().Count);
                if (claims.ToList().Count == 0 && !user.IsAdmin)
                {
                    claims =
                        [
                            new Claim(ClaimTypes.Name, user.UserName ?? ""),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Email, user.Email ?? ""),
                            new Claim("profileId", user.Id),
                        ];

                }
                else if ((claims == null || claims.ToList().Count == 0) && user.IsAdmin)
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

                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.GivenName, user.FirstName??""),
                        new Claim(AppClaimType.ProfileId, user.Id.ToString()),
                        new Claim(ClaimTypes.Surname,  user.LastName),
                        new Claim(ClaimTypes.Email,  user.Email),
                        new Claim(AppClaimType.SecurityStamp, user.SecurityStamp)
                    }
                    .Union(userClaim)
                    .Union(roleClaim)
                    .Union(permissionClaim);
                    #endregion


                    await userManager.AddClaimsAsync(user, claims);
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); 

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(24),
                    SigningCredentials = creds,
                    Issuer = configuration["JwtConfig:Issuer"]!
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var accessToken = tokenHandler.WriteToken(token);
                logger.LogInformation("User Access Token {0}", accessToken);
                return accessToken;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Generate token failed");
                return "failedtoken";
            }
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        }
    }
}
