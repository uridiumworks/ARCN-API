namespace ARCN.Application.DataModels.UserProfile
{
    public class ForgotPasswordMail
    {
        public string Email { get; set; }
        public string? Name { get; set; }
        public string Token { get; set; }
        public string Url { get; set; }
    }
}
