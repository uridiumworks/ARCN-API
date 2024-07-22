using ARCN.Application.DataModels.Security2fa;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Enums;
using AutoMapper;

namespace ARCN.Repository.Repositories
{
    public class SecurityQuestionRepository : GenericRepository<SecurityQuestion>, ISecurityQuestionRepository
    {
        private readonly IMapper mapper;
        private readonly IUserIdentityService userIdentityService;

        public SecurityQuestionRepository(ARCNDbContext dbContext, IMapper mapper, IUserIdentityService userIdentityService) : base(dbContext)
        {
            this.mapper = mapper;
            this.userIdentityService = userIdentityService;
        }

        public async ValueTask<SecurityQuestionBasedOnCategoryDataModel> GetQuestionsByCategory()
        {
            var res = FindAll();

            var questions = new SecurityQuestionBasedOnCategoryDataModel();

            foreach (var item in res)
            {
                if (item.Category == SecurityQuestionCategories.FamilyFriends)
                {
                    var data = mapper.Map<SecurityQuestionResDataModel>(item);

                    questions.FamilyFriends.Add(data);
                }
                else if (item.Category == SecurityQuestionCategories.Education)
                {
                    var data = mapper.Map<SecurityQuestionResDataModel>(item);

                    questions.Education.Add(data);
                }
                else if (item.Category == SecurityQuestionCategories.FoodsAndHousePets)
                {
                    var data = mapper.Map<SecurityQuestionResDataModel>(item);

                    questions.FoodsAndHousePets.Add(data);
                }


            }

            return questions;
        }
    }
}
