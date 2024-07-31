using System.Security.Cryptography;
using ARCN.Application.DataModels.Identity;
using ARCN.Application.Interfaces.Services;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public TokenService(IConfiguration configuration,
            UserManager<ApplicationUser> userManager
           ,RoleManager<IdentityRole> roleManager
            )
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }



        public async ValueTask<string> CreateTokenAsync(ApplicationUser user)
        {
            IEnumerable<Claim> claims = await userManager.GetClaimsAsync(user);
            if ((claims == null || claims.ToList().Count == 0) && !string.IsNullOrEmpty(user.Email) && user.IsAdmin!=true)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                };
                await userManager.AddClaimsAsync(user, claims);
            }
            else if ((claims == null || claims.ToList().Count == 0) && user.IsAdmin!=true)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };
                await userManager.AddClaimsAsync(user, claims);
            }
            else if ((claims == null || claims.ToList().Count == 0) && user.IsAdmin==true)
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
                   // new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("StaffName", user.FirstName),
                    new Claim("email", user.Email),
                }
                .Union(userClaim)
                .Union(roleClaim)
                .Union(permissionClaim);
                #endregion


                await userManager.AddClaimsAsync(user, claims);
            }
            else if ((claims != null || claims.ToList().Count > 0) && user.IsAdmin != true)
            {
                claims = new List<Claim>
                {
                        new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim("ComfrimPhoneNumber",user.PhoneNumberConfirmed.ToString()),
                };
                await userManager.AddClaimsAsync(user, claims);
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds,
                Issuer = configuration["JwtConfig:Issuer"]!
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

       

        public async ValueTask<TokenResponse> CreateTokenAsync(TokenRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return default;

            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid) return default;

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryDate = DateTime.Now.AddDays(7);

            await userManager.UpdateAsync(user);

            var token = await GenerateJwtAsync(user);

            return new TokenResponse
            {
                Token = token,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = (DateTime)user.RefreshTokenExpiryDate
            };


        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateJwtAsync(ApplicationUser user)
        {
            var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));

            return token;
        }

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(25),
                signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]);

            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser applicationUser)
        {
            var userClaim = await userManager.GetClaimsAsync(applicationUser);
            var roles = await userManager.GetRolesAsync(applicationUser);

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
                new (ClaimTypes.Name, applicationUser.UserName),
                new (ClaimTypes.NameIdentifier, applicationUser.Id),
                new (ClaimTypes.MobilePhone, applicationUser.PhoneNumber),
            }
            .Union(userClaim)
            .Union(roleClaim)
            .Union(permissionClaim);

            return claims;
        }

        public ValueTask<TokenResponse> CreateRefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
