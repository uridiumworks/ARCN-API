using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class SecurityQuestionAnswer : AuditableEntity
    {
        public int SecurityQuestionAnswerId { get; set; }
        public int SecurityQuestionId { get; set; }
        public string? Answer { get; set; }
        public virtual SecurityQuestion? SecurityQuestion { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
