using ARCN.Application.DataModels.Identity;
using ARCN.Application.DataModels.UserProfile;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ARCN.Application.Interfaces.Services
{
    public interface IUserService
    {
        ValueTask AddUserClaimsAsync(ApplicationUser user);
        ValueTask<UserResponseDataModel> AdminUserResponses(ApplicationUser user);
        ValueTask<string> GenerateNewToken(ApplicationUser user);
        ValueTask<RoleClaimResponseDataModel> GetPermissionAsync(string roleId);
        ValueTask<List<RoleClaimResponseDataModel>> GetAllRoleClaims();
        ValueTask<bool> UpdatePermissionAsync(UpdateRolePermissionRequestDataModel dataModel);
        ValueTask<List<ApplicationRoleClaim>> GetAllClaims();
        ValueTask AddPermissionToRole(List<RoleClaimDataModel> roleClaimDataModels, string roleId);
        ValueTask<List<RoleClaimDataModel>> GetAllUnAssignedClaims();
    }
}
