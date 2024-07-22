using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.Security2fa
{
    public class SecurityQuestionBasedOnCategoryDataModel
    {
        public List<SecurityQuestionResDataModel> FamilyFriends { get; set; } = new List<SecurityQuestionResDataModel>();
        public List<SecurityQuestionResDataModel> Education { get; set; } = new List<SecurityQuestionResDataModel>();
        public List<SecurityQuestionResDataModel> FoodsAndHousePets { get; set; } = new List<SecurityQuestionResDataModel>();
    }
}
