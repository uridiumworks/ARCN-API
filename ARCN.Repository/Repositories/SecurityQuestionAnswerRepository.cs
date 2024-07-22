using ARCN.Application.Interfaces.Services;


namespace ARCN.Repository.Repositories
{
    public class SecurityQuestionAnswerRepository : GenericRepository<SecurityQuestionAnswer>, ISecurityQuestionAnswerRepository
    {
        private readonly IUserIdentityService userIdentityService;
        private readonly UserManager<ApplicationUser> userManager;

        public SecurityQuestionAnswerRepository(ARCNDbContext dbContext, IUserIdentityService userIdentityService, UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            this.userIdentityService = userIdentityService;
            this.userManager = userManager;
        }

        public async ValueTask<SecurityQuestionAnswer> CreateSecurityAnswer(SecurityQuestionAnswer Answer)
        {
            Answer.ApplicationUserId = userIdentityService.UserId;
            var res = Table.Add(Answer);

            return res.Entity;
        }
        public async ValueTask<SecurityQuestionAnswer> GetSecurityAnswerByQuestion(int questionId)
        {
            var res = await Table.Where(x => x.SecurityQuestionId == questionId && x.ApplicationUserId == userIdentityService.UserId).FirstOrDefaultAsync();
            return res;
        }

        public async ValueTask<bool> UpdateSecurityAnswer(int key, SecurityQuestionAnswer Answer)
        {
            var securityAns = await Table.FindAsync(key);
            if (securityAns == null) return false;

            securityAns.Answer = Answer.Answer;

            this.Update(securityAns);

            return true;


        }

        public bool ValidateSecurityQuestionAndAnswers(Dictionary<int, string> questionIdAndAnswer, string profileId, ApplicationUser user)
        {
            bool isValid = true;
            var questionId = questionIdAndAnswer.Select(x => x.Key).ToList();
            var securityQuestionAnswer = Table.Where(x => x.ApplicationUserId == profileId && questionId.Contains(x.SecurityQuestionId)).ToList();

            foreach (var quest in securityQuestionAnswer)
            {
                var answer = questionIdAndAnswer[quest.SecurityQuestionId];
                PasswordVerificationResult result = userManager.PasswordHasher.VerifyHashedPassword(user, quest.Answer, answer.ToUpper());

                if (result != PasswordVerificationResult.Success)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;

        }
    }
}
