namespace ARCN.Application.Interfaces.Repositories
{
    public interface ISecurityQuestionAnswerRepository : IGenericRepository<SecurityQuestionAnswer>
    {
        ValueTask<SecurityQuestionAnswer> CreateSecurityAnswer(SecurityQuestionAnswer Answer);
        ValueTask<bool> UpdateSecurityAnswer(int key, SecurityQuestionAnswer Answer);
        ValueTask<SecurityQuestionAnswer> GetSecurityAnswerByQuestion(int questionId);
        bool ValidateSecurityQuestionAndAnswers(Dictionary<int, string> questionIdAndAnswer, string profileId, ApplicationUser user);
    }
}
