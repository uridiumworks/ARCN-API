using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces
{
    public interface IEmailService
    {
        ValueTask ForgotPasswordMail(ForgotPasswordMail forgotPasswordMail);
        ValueTask ConfirmEmailAddress(string token, ApplicationUser user);
        void SendValidationCodeMail(ApplicationUser user, string code);
        ValueTask PasswordReset(ApplicationUser user, string otp);

    }
}
