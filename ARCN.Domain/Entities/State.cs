using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class State:AuditableEntity
    {
        public int StateId { get; set; }
        public string? StateName { get; set; }

    }
}
