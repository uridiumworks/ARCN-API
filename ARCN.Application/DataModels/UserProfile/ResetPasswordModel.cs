
namespace ARCN.Application.DataModels.UserProfile
{
    public class ResetPasswordModel
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Token { get; set; }

    }
    public class ResetForgotPasswordModel
    {
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Token { get; set; }

    }
}