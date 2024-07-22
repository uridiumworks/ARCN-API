
using ARCN.Application.DataModels.Security2fa;

namespace ARCN.Application.DataModels.UserProfile
{
    public class ForgotPassword
    {
        public string? email { get; set; }
        public List<SecurityAnsweDataModel> SecurityQuestionAnswers { get; set; } = new List<SecurityAnsweDataModel>();
    }
}
