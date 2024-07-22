
using System.Threading;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IRegisterService
    {
        ValueTask<ResponseModel<RegisteredUserResponseDataModel>> NewUserAsync(NewUserDataModel userData, CancellationToken cancellationToken);
        ValueTask<ResponseModel<ApplicationUser>> ResetPasswordAsync(ResetPasswordModel resetPassword);
        ValueTask<ResponseModel<string>> ForgotPasswordAsync(ForgotPassword forgotPassword);

        ValueTask<ResponseModel<UserResponseDataModel>> LoginAsync(LoginDataModel login);
        ValueTask<string> VerifyOtp(VerifyOTPDataModel otp);
        ValueTask<string> ResendOTP();
        ValueTask<bool> ValidateRefreshToken(string refreshToken, string userId);
    }
}
