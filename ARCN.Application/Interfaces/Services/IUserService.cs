using ARCN.Application.DataModels.UserProfile;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ARCN.Application.Interfaces.Services
{
    public interface IUserService
    {
        ValueTask AddUserClaimsAsync(ApplicationUser user, List<Claim> claims);
        ValueTask<UserResponseDataModel> UserResponses(ApplicationUser user);
        ValueTask<(IdentityResult, ApplicationUser?)> CreateOrUpdateUserAsync(ApplicationUser applicationUser, string password);
        ApplicationUser CreateNewUser(NewUserDataModel userData, bool IsNewUser);

  
    }
}
