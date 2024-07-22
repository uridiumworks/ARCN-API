
using ARCN.Domain.Commons;
using ARCN.Domain.Enums;

namespace ARCN.Domain.Entities
{
    public class SecurityQuestion : AuditableEntity
    {
        public SecurityQuestion()
        {
            SecurityQuestionAnswers = new HashSet<SecurityQuestionAnswer>();
        }
        public int SecurityQuestionId { get; set; }
        public string Question { get; set; }
        public SecurityQuestionCategories Category { get; set; }
        public virtual ICollection<SecurityQuestionAnswer> SecurityQuestionAnswers { get; set; }
    }
}
