using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IUserprofileService
    {
        ValueTask<ResponseModel<ApplicationUser>> UpdateUserProfile(int id, ProfileUpdateDataModel dataModel);
        ValueTask<ResponseModel<ApplicationUser>> GetProfile();
        IQueryable<ApplicationUser> GetAll();
        IQueryable<ApplicationUser> GetById(string id);
        ValueTask<ApplicationUser> GetUserProfile();

    }
}
