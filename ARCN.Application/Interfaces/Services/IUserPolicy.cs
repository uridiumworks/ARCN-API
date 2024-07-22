using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IUserPolicy
    {
        ValueTask<ResponseModel<dynamic>> AuthenticatePinOrTouch(ApplicationUser applicationUser, string pin = "", string touchId = "", bool transaction = true);
        ValueTask<ResponseModel<dynamic>> ValidatePasswordHistory(string userProfileId, string passwordHash);
        ValueTask AddPasswordHistory(string userProfileId, string passwordHash, string userName);
    }
}
