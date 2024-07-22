using ARCN.Application.DataModels.Security2fa;
using System.Threading.Tasks;

namespace ARCN.Application.Interfaces.Repositories
{
    public interface ISecurityQuestionRepository : IGenericRepository<SecurityQuestion>
    {
        ValueTask<SecurityQuestionBasedOnCategoryDataModel> GetQuestionsByCategory();
    }
}
