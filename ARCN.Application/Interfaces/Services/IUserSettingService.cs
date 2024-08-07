

using ARCN.Application.DataModels.Identity;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IUserSettingService
    {
        ValueTask<ResponseModel<ApplicationUser>> GetProfile();
        ValueTask<ResponseModel<List<ApplicationUser>>> GetAllUsers();
        ValueTask<ResponseModel<UserResponseDataModel>> CreateAdminUser(NewUserDataModel User);
        ValueTask<ResponseModel<ApplicationUser>> UpdateUser(NewUserDataModel User);
        ValueTask<bool> RemoveAdminUser(string id);
        ValueTask<ResponseModel<RoleClaimResponseDataModel>> GetPermissionAsync(string roleId);
        ValueTask<ResponseModel<List<RoleClaimResponseDataModel>>> GetAllRoleClaims();
        ValueTask<bool> UpdatePermissionAsync(UpdateRolePermissionRequestDataModel dataModel);
        ValueTask<ResponseModel<List<ApplicationRoleClaim>>> GetAllClaims();
        ValueTask AddPermissionToRole(List<RoleClaimDataModel> roleClaimDataModels, string roleId);
        ValueTask<ResponseModel<List<RoleClaimDataModel>>> GetAllUnAssignedClaims();
    }
}
